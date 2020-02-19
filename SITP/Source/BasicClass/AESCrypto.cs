using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SITP
{
    public class AESCrypto
    {
          
            ///////////////////////////////////////
            //  Key Definition
            ///////////////////////////////////////
            //String keyValue = "12345678901234567890123456789012"
            //String IV = "HeungkukArcSys12"


            #region Private/Protected Member Variables

            /// <summary>        
            /// Decryptor        
            ///         
            private readonly ICryptoTransform _decryptor;

            /// <summary>        
            /// Encryptor        
            ///         
            private readonly ICryptoTransform _encryptor;

            /// <summary>        
            /// 16-byte Private Key        
            ///         
            private static readonly byte[] IV = Encoding.UTF8.GetBytes(CommonData.IVData);

            /// <summary>        
            /// Public Key        
            ///         
            private readonly byte[] _password;

            /// <summary>        
            /// Rijndael cipher algorithm        
            ///         
            private readonly RijndaelManaged _cipher;

            #endregion

            #region Private/Protected Properties

            private ICryptoTransform Decryptor { get { return _decryptor; } }
            private ICryptoTransform Encryptor { get { return _encryptor; } }

            #endregion

            #region Private/Protected Methods
            #endregion

            #region Constructor

            /// <summary>        
            /// Constructor        
            ///         
            /// <param name="password">Public key        
            public AESCrypto(string password)
            {
                //Encode digest            
                var md5 = new MD5CryptoServiceProvider();
                _password = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

                //Initialize objects            
                _cipher = new RijndaelManaged();
                _decryptor = _cipher.CreateDecryptor(_password, IV);
                _encryptor = _cipher.CreateEncryptor(_password, IV);
            }

            #endregion
            #region Public Properties
            #endregion

            #region Public Methods

            /// <summary>        
            /// Decryptor        
            ///         
            /// <param name="text">Base64 string to be decrypted        
            /// <returns>        
            public string Decrypt(string text)
            {
                try
                {
                    byte[] input = Convert.FromBase64String(text);
                    var newClearData = Decryptor.TransformFinalBlock(input, 0, input.Length);
                    return Encoding.UTF8.GetString(newClearData);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }
            }

            /// <summary>        
            /// Decryptor        
            ///         
            /// <param name="text">Base64 string to be decrypted        
            /// <returns>        
            public string Decrypt(byte[] input)
            {
                try
                {
                    var newClearData = Decryptor.TransformFinalBlock(input, 0, input.Length);
                    return Encoding.UTF8.GetString(newClearData);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }
            }

            /// <summary>        
            /// Encryptor        
            ///         
            /// <param name="text">String to be encrypted        
            /// <returns>        
            /// 
            public string Encrypt(string text)
            {
                try
                {
                    //var buffer = Encoding.ASCII.GetBytes(text);
                    var buffer = Encoding.UTF8.GetBytes(text);
                    return Convert.ToBase64String(Encryptor.TransformFinalBlock(buffer, 0, buffer.Length));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }
            }

            /// <summary>        
            /// Encryptor        
            ///         
            /// <param name="text">String to be encrypted        
            /// <returns>        
            /// 
            public byte[] Encrypt(byte[] buffer)
            {
                try
                {
                    return Encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae);
                    return null;
                }
                catch (ObjectDisposedException oe)
                {
                    Console.WriteLine("The object has already been disposed." + oe);
                    return null;
                }
            }

            #endregion

    }

        /*----------------------------------------------------------------------------------------------------------------------------------*/
        // Java Sample
        /*----------------------------------------------------------------------------------------------------------------------------------*/
    /* package com.chkss108.security; 

    import android.util.Log;
    import android.util.Base64; 
    import java.security.InvalidAlgorithmParameterException;
    import java.security.InvalidKeyException;
    import java.security.MessageDigest;
    import java.security.NoSuchAlgorithmException; 
    import javax.crypto.BadPaddingException;
    import javax.crypto.Cipher;
    import javax.crypto.IllegalBlockSizeException;
    import javax.crypto.NoSuchPaddingException;
    import javax.crypto.SecretKey;
    import javax.crypto.spec.IvParameterSpec;
    import javax.crypto.spec.SecretKeySpec; 

    public class RijndaelCrypt 
    {     
        public static final String TAG = "YourAppName";     
        private static String TRANSFORMATION = "AES/CBC/PKCS7Padding";    
        private static String ALGORITHM = "AES";    
        private static String DIGEST = "MD5";         
        private static Cipher _cipher;    
        private static SecretKey _password;    
        private static IvParameterSpec _IVParamSpec;         
        //16-byte private key    
        private static byte[] IV = "ArchBROKERSERVER".getBytes();              
            
        public RijndaelCrypt(String password) 
        {         
            try 
            {                         
                //Encode digest            
                MessageDigest digest;                       
                digest = MessageDigest.getInstance(DIGEST);                        
                _password = new SecretKeySpec(digest.digest(password.getBytes()), ALGORITHM);                         
                    
                //Initialize objects            
                _cipher = Cipher.getInstance(TRANSFORMATION);            
                _IVParamSpec = new IvParameterSpec(IV);                     
            } 
            catch (NoSuchAlgorithmException e) 
            {            
                Log.e(TAG, "No such algorithm " + ALGORITHM, e);        
            } 
            catch (NoSuchPaddingException e) 
            {            
                Log.e(TAG, "No such padding PKCS7", e);        
            }                  
        }     
            
        public String encrypt(byte[] text) 
        {                 
            byte[] encryptedData;                 
            try {                         
                _cipher.init(Cipher.ENCRYPT_MODE, _password, _IVParamSpec);            
                encryptedData = _cipher.doFinal(text);                     
            } 
            catch (InvalidKeyException e) 
            {            
                Log.e(TAG, "Invalid key  (invalid encoding, wrong length, uninitialized, etc).", e);            
                return null;        
            } 
            catch (InvalidAlgorithmParameterException e) 
            {            
                Log.e(TAG, "Invalid or inappropriate algorithm parameters for " + ALGORITHM, e);            
                return null;        
            } 
            catch (IllegalBlockSizeException e) 
            {            
                Log.e(TAG, "The length of data provided to a block cipher is incorrect", e);            
                return null;        
            } 
            catch (BadPaddingException e) 
            {            
                Log.e(TAG, "The input data but the data is not padded properly.", e);            
                return null;        
            }                                
            return Base64.encodeToString(encryptedData,Base64.DEFAULT);             
        }        
 
        public String decrypt(String text) 
        {         
            try 
            {            
                _cipher.init(Cipher.DECRYPT_MODE, _password, _IVParamSpec);                         
                byte[] decodedValue = Base64.decode(text.getBytes(), Base64.DEFAULT);            
                byte[] decryptedVal = _cipher.doFinal(decodedValue);            
                return new String(decryptedVal);                                              
            } 
            catch (InvalidKeyException e) 
            {            
                Log.e(TAG, "Invalid key  (invalid encoding, wrong length, uninitialized, etc).", e);            
                return null;        
            } 
            catch (InvalidAlgorithmParameterException e) 
            {            
                Log.e(TAG, "Invalid or inappropriate algorithm parameters for " + ALGORITHM, e);            
                return null;        
            } 
            catch (IllegalBlockSizeException e) 
            {            
                Log.e(TAG, "The length of data provided to a block cipher is incorrect", e);            
                return null;        
            } 
            catch (BadPaddingException e) 
            {            
                Log.e(TAG, "The input data but the data is not padded properly.", e);            
                return null;        
            }                            
        }     
    } */
    /*----------------------------------------------------------------------------------------------------------------------------------*/
        // Java Sample
        /*----------------------------------------------------------------------------------------------------------------------------------*/

}
