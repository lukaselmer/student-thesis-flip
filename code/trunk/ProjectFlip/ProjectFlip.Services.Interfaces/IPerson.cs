namespace ProjectFlip.Services.Interfaces
{
    public interface IPerson
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        string ImageUrl { get; }
    }
}