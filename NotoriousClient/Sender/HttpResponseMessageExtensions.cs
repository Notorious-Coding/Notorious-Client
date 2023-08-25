using System.Net.Http.Formatting;

namespace NotoriousClient.Framework.Web.Client.Sender
{
    /// <summary>
    /// Méthodes d'extensions sur la classe <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        private static readonly IList<MediaTypeFormatter> MediaTypeFormatters = new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter()};

        /// <summary>
        /// Permet de mapper le contenu d'une réponse à un objet de manière asynchrone.
        /// </summary>
        /// <typeparam name="T">Type de l'objet de retour.</typeparam>
        /// <param name="response">La réponse a lire.</param>
        /// <returns>Le contenu de la réponse mappé.</returns>
        public static Task<T> ReadAsAsync<T>(this HttpResponseMessage response)
        {
            return response.Content.ReadAsAsync<T>(MediaTypeFormatters);
        }

        /// <summary>
        /// Permet de lire le contenu de la réponse comme une chaine de caractère de manière asynchrone.
        /// </summary>
        /// <param name="response">La réponse a lire.</param>
        /// <returns>Le contenu de la réponse au format String</returns>
        public static Task<string> ReadAsStringAsync(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Permet de lire le contenu de la réponse comme un tableau de byte de manière asynchrone.
        /// </summary>
        /// <param name="response">La réponse a lire.</param>
        /// <returns>Le contenu de la réponse au format d'un tableau de byte.</returns>
        public static Task<byte[]> ReadAsByteArrayAsync(this HttpResponseMessage response)
        {
            return response.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        /// Permet de lire le contenu de la réponse comme un stream de manière asynchrone.
        /// </summary>
        /// <param name="response">La réponse a lire.</param>
        /// <returns>Le contenu de la réponse au format d'un stream.</returns>
        public static Task<System.IO.Stream> ReadAsStreamAsync(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStreamAsync();
        }

        /// <summary>
        /// Permet de mapper le contenu de la réponse à un objet.
        /// </summary>
        /// <typeparam name="T">Type de l'objet de retour.</typeparam>
        /// <param name="response">La réponse à lire.</param>
        /// <returns>Le contenu de la réponse mappé.</returns>
        public static T ReadAs<T>(this HttpResponseMessage response)
        {
            return ReadAsAsync<T>(response).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Permet de lire le contenu de la réponse comme une chaine de caractère.
        /// </summary>
        /// <param name="response">La réponse a lire.</param>
        /// <returns>Le contenu de la réponse au format d'une string.</returns>
        public static string ReadAsString(this HttpResponseMessage response)
        {
            return ReadAsStringAsync(response).Result;
        }

        /// <summary>
        /// Permet de lire le contenu de la réponse au format tableau de byte.
        /// </summary>
        /// <param name="response">La réponse a lire.</param>
        /// <returns>Le contenu de la réponse au format d'un tableau de byte.</returns>
        public static byte[] ReadAsByteArray(this HttpResponseMessage response)
        {
            return ReadAsByteArrayAsync(response).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Permet de lire le contenu de la réponse comme un stream.
        /// </summary>
        /// <param name="response">La réponse a lire.</param>
        /// <returns>Le contenu de la réponse au format d'un stream.</returns>
        public static System.IO.Stream ReadAsStream(this HttpResponseMessage response)
        {
            return ReadAsStreamAsync(response).GetAwaiter().GetResult();
        }
    }
}
