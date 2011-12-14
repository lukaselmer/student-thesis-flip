using System.Security.Cryptography;
using System.Text;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class Person : IPerson
    {

        #region Constructor

        public Person(string name, string email)
        {
            Name = name;
            Email = email;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string ImageUrl { get { return string.Format("http://www.gravatar.com/avatar/{0}?s=150", Md5Hash(Email)); } }

        #endregion

        #region Other

        private static string Md5Hash(object email)
        {
            var inputBytes = Encoding.ASCII.GetBytes(email.ToString());
            var hash = MD5.Create().ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            var hashedString = sb.ToString();
            return hashedString;
        }
        #endregion

    }
}