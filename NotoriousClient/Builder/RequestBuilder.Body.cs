using NotoriousClient.Converters;
using System.Text;

namespace NotoriousClient.Builder
{
    public partial class RequestBuilder : IRequestBuilder
    {
        /// <summary>
        /// Default JSON Converter.
        /// </summary>
        protected IJsonConverter DefaultJsonConverter = new NewtonsoftJsonConverter();

        private List<Body> _bodies = new List<Body>();
        private bool? _isMultipartRequest = null;

        ///<inheritdoc/>
        public IRequestBuilder WithJsonBody(object data, IJsonConverter? converter = null)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            SetIsMultipart(isMultipart: false);
            var body = new Body(new StringContent(GetConverter(converter).ConvertToJson(data), Encoding.UTF8, "application/json"));

            _bodies.Insert(0, body);
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder WithStreamBody(Stream body)
        {
            ArgumentNullException.ThrowIfNull(body, nameof(body));
            SetIsMultipart(isMultipart: false);
            _bodies.Insert(0, new Body(new StreamContent(body)));
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder WithContentBody(HttpContent content)
        {
            ArgumentNullException.ThrowIfNull(content, nameof(content));
            SetIsMultipart(isMultipart: false);

            _bodies.Insert(0, new Body(content));
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder WithStreamMultipartBody(Stream data, string section)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            SetIsMultipart(isMultipart: true);


            _bodies.Add(new Body(new StreamContent(data), section));
            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder WithJsonMultipartBody(object data, string section, IJsonConverter? converter = null)
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            if (converter == null)
                converter = DefaultJsonConverter;

            SetIsMultipart(isMultipart: true);


            _bodies.Add(new Body(new StringContent(GetConverter(converter).ConvertToJson(data)), section));

            return this;
        }

        ///<inheritdoc/>
        public IRequestBuilder WithContentMultipartBody(HttpContent content, string section)
        {
            ArgumentNullException.ThrowIfNull(content, nameof(content));

            SetIsMultipart(isMultipart: true);
            _bodies.Add(new Body(content, section));
            return this;
        }

        #region Private Methods
        private void BuildBody(HttpRequestMessage request)
        {
            if (_isMultipartRequest == true)
            {
                var content = new MultipartFormDataContent();
                foreach (var body in _bodies)
                {
                    content.Add(body.Content, body.Section);
                }
                request.Content = content;
            }
            else if(_isMultipartRequest == false)
            {
                request.Content = _bodies[0].Content;
            }
        }

        private IJsonConverter GetConverter(IJsonConverter? converter = null)
        {
            return converter == null ? DefaultJsonConverter : converter;
        }

        private void SetIsMultipart(bool isMultipart)
        {
            if (_isMultipartRequest != null && _isMultipartRequest == !isMultipart)
                throw new BodyNotCompatibleException();
            else
                _isMultipartRequest = isMultipart;
        }
        #endregion
    }
}
