namespace ProjectFlip.Services.Interfaces
{
    /// <summary>
    /// Interface to register the language "und".
    /// </summary>
    /// <remarks></remarks>
    public interface ICultureHelper
    {
        /// <summary>
        /// Registers the language.
        /// Necessary because XPS documents use the language "und" which causes an exception when it's not registered.
        /// </summary>
        void RegisterLanguage();
    }
}