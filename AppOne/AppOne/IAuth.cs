using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppOne
{
    public interface IAuth
    {
        Task<String> LoginWithEmailAndPassword(string Email, string Password);
        Task<String> SignUpWithEmailAndPassword(string Email, string Password);

        bool SignOut();
        bool IsSignIn();
    }
}
