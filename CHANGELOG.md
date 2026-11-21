# Changelog

## 3.0.0
### 💥 Breaking changes

- Dropped .NET 8 support (out of support since November 2026): the package now targets .NET 10 only.
- Removed dependency on 'Microsoft.AspNet.WebApi.Client' (and transitively Newtonsoft.Json), which was not trimming / AOT compatible.
- 'HttpResponseMessageExtensions.ReadAsAsync<T>' and 'ReadAs<T>' now use System.Text.Json: the 'IList<MediaTypeFormatter>' parameter is replaced by an optional 'JsonSerializerOptions' (defaults to case-insensitive Web defaults).
- 'IRequestBuilder.WithJsonBody' / 'WithJsonMultipartBody': the optional 'IJsonSerializer? converter' parameter is replaced by two overloads. The overloads without serializer are annotated as not trimming / AOT safe.
- 'RequestBuilder.DefaultJsonConverter' is now nullable, and the parameterless 'SystemTextJsonSerializer' constructor is annotated as not trimming / AOT safe.

### 🛠 Technical

- Target .NET 10 again.
- Added 'ReadAsAsync<T>(JsonTypeInfo<T>)' / 'ReadAs<T>(JsonTypeInfo<T>)' overloads for trimmed / native AOT applications.
- Added 'WithJsonBody<T>(T, JsonTypeInfo<T>)' / 'WithJsonMultipartBody<T>(T, string, JsonTypeInfo<T>)' extensions and a 'SystemTextJsonSerializer(JsonSerializerContext)' constructor for trimmed / native AOT applications. The library no longer emits any trimming / AOT warning.
- Client credentials token client now uses System.Text.Json source generation.
- Tests migrated to xunit v3.

### 🐛 Bug Fixes

- Fixed a 'FormatException' when building a request combining endpoint parameters ('/users/{id}') and query parameters.
- Fixed 'TokenClient' / 'MemoryCachedTokenClient' always throwing an 'ArgumentNullException' when requesting a token. A failed token request now throws an 'HttpRequestException'.

## 2.1.0
### 🛠 Technical

- Upgrade to .NET 10
- Added 'ClientCredentialsBaseClient' support.
- Added possibility to pass MediaTypeFormatters to 'HttpResponseMessageExtensions.ReadAsAsync<T>'

## 2.0.0

### 🛠 Technical

- Upgrade to .NET 8
- Switch to System.Text.Json to improve performance.


## 2.0.1

### 🛠 Technical

- Minor bug fixes on tools (CI, csproj, etc...)

## 2.0.2

### 🐛 Bug Fixes

- Fixed a bug where the entire query string was encoded instead of only the query parameter keys and values.

## 2.0.3

### 🛠 Technical

- Fixed HttpClientFactory usage in default request sender
- Migrate to SLNX

## 2.1.0

### ✨ Features

- Added support for Versioned API routes via `VersionedEndpoint` class, and `GetBuilder(VersionedEndpoint endpoint)` method signatures.
- Added support for ClientCredentials OAuth authentication flow via `ClientCredentialsBaseClient` and `CachedClientCredentialsBaseClient`.

### 🛠 Technical

- Overriding `GetBuilder` now changes the behaviour of `GetBuilderAsync`.