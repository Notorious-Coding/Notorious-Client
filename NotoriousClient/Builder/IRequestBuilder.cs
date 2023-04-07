using NotoriousClient.Framework.Web.Client.Builder.Authentication;
using NotoriousClient.Framework.Web.Client.Converters;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace NotoriousClient.Framework.Web.Client.Builder
{
    /// <summary>
    /// Classe permettant de construire une requête HTTP de toute part.
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// Permet d'ajouter un corps au format JSON à la requête.
        /// </summary>
        /// <param name="body">Informations a mettre dans le corps en json de la requête.</param>
        /// <param name="converter">Format du corps de la requête.</param>
        IRequestBuilder WithJsonBody(object body, IJsonConverter? converter = null);

        /// <summary>
        /// Permet d'ajouter un corps au format Stream à la requête.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        IRequestBuilder WithStreamBody(Stream body);

        /// <summary>
        /// Permet d'ajouter un corps HTTP a la requête.
        /// </summary>
        /// <param name="content">Contenu HTTP à utiliser pour la requête.</param>
        IRequestBuilder WithContentBody(HttpContent content);

        /// <summary>
        /// Permet d'ajouter un fichier au format stream au sein de la requête Multipart.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        IRequestBuilder WithStreamMultipartBody(Stream body, string section);

        /// <summary>
        /// Permet d'ajouter un corps au format JSON à la requête Multipart.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="section"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        IRequestBuilder WithJsonMultipartBody(object body, string section, IJsonConverter? converter = null);

        /// <summary>
        /// Permet d'ajouter un corps HTTP à la requête multipart.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        IRequestBuilder WithContentMultipartBody(HttpContent content, string section);


        /// <summary>
        /// Ajouter une entête personalisée à la requête.
        /// </summary>
        /// <param name="key">Clé de l'entête.</param>
        /// <param name="value">Valeur de l'entête.</param>
        IRequestBuilder AddCustomHeader(string key, string value);

        /// <summary>
        /// Permet d'ajouter plusieurs paramètres GET dans l'url.
        /// </summary>
        /// <param name="headers">Listes des headers à ajouter.</param>
        /// <returns></returns>
        IRequestBuilder AddCustomHeaders(Dictionary<string, string> headers);

        /// <summary>
        /// Permet d'ajouter plusieurs paramètres GET dans l'url.
        /// </summary>
        /// <param name="headers">Listes des headers à ajouter.</param>
        /// <returns></returns>
        IRequestBuilder AddCustomHeaders(IDictionary<string, string> headers);

        /// <summary>
        /// Permet d'ajouter une entête "Accept" afin de préciser au serveur le type de retour accepté.
        /// </summary>
        /// <param name="type">Type de retour accepté.</param>
        IRequestBuilder WithCustomAcceptMediaType(string type);

        /// <summary>
        /// Permet de s'authentifier a partir d'un token dans le header Authorization.
        /// </summary>
        /// <param name="auth">Objet representant l'authentication utilisé.</param>
        /// <returns></returns>
        IRequestBuilder WithAuthentication(IAuthenticationInformation auth);

        /// <summary>
        /// Permet d'ajouter un paramètre GET dans l'url.
        /// </summary>
        /// <param name="key">Nom du paramètre.</param>
        /// <param name="value">Valeur du paramètre.</param>
        /// <returns></returns>
        IRequestBuilder AddQueryParameter(string key, string value);

        /// <summary>
        /// Permet d'ajouter plusieurs paramètres GET dans l'url.
        /// </summary>
        /// <param name="queryParams">Dictionnaire des paramètres.</param>
        /// <returns></returns>
        IRequestBuilder AddQueryParameters(Dictionary<string, string> queryParams);

        /// <summary>
        /// Permet d'ajouter plusieurs paramètres GET dans l'url.
        /// </summary>
        /// <param name="queryParams">Dictionnaire des paramètres.</param>
        /// <returns></returns>
        IRequestBuilder AddQueryParameters(IDictionary<string, string> queryParams);

        /// <summary>
        /// Permet d'interpoler la <paramref name="key"/> entourée de { } au sein de l'endpoint par la <paramref name="value"/>.
        /// </summary>
        /// <param name="key">Clé de remplacement.</param>
        /// <param name="value">Valeur du paramètre.</param>
        /// <returns></returns>
        IRequestBuilder AddEndpointParameter(string key, string value);

        /// <summary>
        /// Permet d'interpoler les clés de <paramref name="endpointParams"/> entourée de { } au sein de l'endpoint par les valeurs de <paramref name="endpointParams"/>.
        /// </summary>
        /// <param name="endpointParams">Dictionnaire de paramètres.</param>
        /// <returns></returns>
        IRequestBuilder AddEndpointParameters(IDictionary<string, string> endpointParams);

        /// <summary>
        /// Permet d'interpoler les clés de <paramref name="endpointParams"/> entourée de { } au sein de l'endpoint par les valeurs de <paramref name="endpointParams"/>.
        /// </summary>
        /// <param name="endpointParams">Dictionnaire de paramètres.</param>
        /// <returns></returns>
        IRequestBuilder AddEndpointParameters(Dictionary<string, string> endpointParams);

        /// <summary>
        /// Récuperer la requête construite.
        /// </summary>
        /// <returns>Requête construite.</returns>
        HttpRequestMessage Build();
    }
}
