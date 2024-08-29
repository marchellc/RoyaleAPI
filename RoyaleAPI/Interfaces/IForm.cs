namespace RoyaleAPI.Interfaces
{
    /// <summary>
    /// An interface used for "forms" - simpler POST & PATCH & DELETE methods.
    /// </summary>
    public interface IForm
    {
        /// <summary>
        /// Validates this form instance.
        /// </summary>
        void ValidateForm();

        /// <summary>
        /// Converts this form instance to a JSON string.
        /// </summary>
        /// <returns>The formatted JSON string.</returns>
        string ToJson();
    }
}