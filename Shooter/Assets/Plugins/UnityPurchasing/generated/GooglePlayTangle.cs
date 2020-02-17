#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("uHIa2jmb+KdIlUyC1dos61eOdEBMFzBsTQFkG1jAgPwOghfnVMJcivR5kQTg50wpKK3TtiKNYgE9vH3rRSo7XUYEs7oqCasv7XQU/xACj51lUm+1pG+B2Zh7KwawiruW0hsOAg5rljFBdnVC/zD7pC2VD2Gx0kzMd2frmmHeTB1JOf8Vzh0kMJHlqBcuEbfMBCYHRKox9L6ap5n5ygY/vr8Njq2/gomGpQnHCXiCjo6Oio+MJF3TOPX4fiTlKxLFHw08RbZa8mUNjoCPvw2OhY0Njo6PAH8ybLN4H2+phardF4EGHlSoDWgEgl4lMNMIqRpxmPO6wxcrCSHEMukgc82a2cuGqvFE3M88neAf816cP464qvS0gzxwW53PeT745I2Mjo+O");
        private static int[] order = new int[] { 6,7,3,5,12,5,9,10,9,12,10,12,12,13,14 };
        private static int key = 143;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
