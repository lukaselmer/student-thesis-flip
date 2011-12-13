namespace ProjectFlip.Services.Interfaces
{
    public interface ICultureHelper
    {
        /// <summary>
        /// Registers the language.
        /// Necessary because XPS documents use the Language "und" which causes an exception when it's not registered.
        /// </summary>
        void RegisterLanguage();
    }
}