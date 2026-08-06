/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using Microsoft.Extensions.DependencyInjection;

namespace PChouse.Utils.Encrypt;

public static class EncryptDI
{
    extension(IServiceCollection serviceCollection)
    {
        /// <summary>
        /// Add encrypt service
        /// </summary>
        public void AddEncryptService()
        {
            serviceCollection.AddSingleton<IEncryption>(services =>
            {
                var keys = services.GetRequiredService<IEncryptKey>();
                return new Encryption(keys);
            });
        }

        /// <summary>
        /// Add encrypt service
        /// </summary>
        /// <param name="encryptKey"></param>
        public void AddEncryptService(Action<IEncryptKey> encryptKey)
        {
            var key = new EncryptKey();
            encryptKey.Invoke(key);
            serviceCollection.AddEncryptService(key);
        }

        /// <summary>
        /// Add encrypt service
        /// </summary>
        /// <param name="encryptKey"></param>
        public void AddEncryptService(IEncryptKey encryptKey)
        {
            serviceCollection.AddSingleton<IEncryption>(services =>
            {
                return new Encryption(encryptKey);
            });
        }

    }

}