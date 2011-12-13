#region

using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media.Imaging;

#endregion

namespace ProjectFlip.UserInterface.Surface.Views
{
    public partial class Gravatar
    {
        public Gravatar()
        {
            InitializeComponent();
        }

        public object PersonName { set { GravatarName.Content = value; } }

        public object Email
        {
            set
            {
                if (value == null || !(value is string)) return;
                var url = string.Format("http://www.gravatar.com/avatar/{0}?s=150", Md5Hash(value));
                GravatarImage.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
            }
        }

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
    }
}