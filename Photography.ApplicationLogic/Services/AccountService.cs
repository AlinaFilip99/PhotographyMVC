using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;
using Photography.ApplicationLogic.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IO;

namespace Photography.ApplicationLogic.Services
{
    public class AccountService
    {
        private readonly  IAccountRepository accountRepository;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(IAccountRepository accountRepository,
            UserManager<Account> userManager, SignInManager<Account> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.accountRepository = accountRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return accountRepository.GetAll();
        }
        public Account GetAccountById(string id)
        {
            return accountRepository.GetById(id);
        }

        public Account AddAccount(Account accountToAdd)
        {
            return accountRepository.Add(accountToAdd);
        }
        public Account UpdateAccount(Account accountToUpdate)
        {
            return accountRepository.Update(accountToUpdate);
        }
        public bool RemoveAccount(string id)
        {
            return accountRepository.Remove(id);
        }

        public async void UpdatePassword(ClaimsPrincipal user, string password)
        {
            Account account=await _userManager.GetUserAsync(user);
            account.PasswordHash = password;
            await _userManager.UpdateAsync(account);
        }

        public bool CheckAccount(string id)
        {
            return accountRepository.Exists(id);
        }
        public IEnumerable<Account> GetAccountsByName(string searchString)
        {
            return accountRepository.GetByName(searchString);
        }

        public async Task<SignInResult> SignInCredentials(string email, string password, bool rememberMe)
        {
            Account account = await _userManager.FindByEmailAsync(email);
            return await _signInManager.PasswordSignInAsync(account.UserName, password, rememberMe, false);
        }

        public async void SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Account> GetUser(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }
        public async Task<IdentityResult> RegisterUser(string nume, string prenume, string email, string profilePath, string password)
        {
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            var user = new Account
            {
                UserName = nume+prenume,
                Email = email,
                Nume = nume,
                Prenume = prenume,
                ProfilePicture = profilePath,
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }
    }
}
