namespace CoTGEnumNetwork.Content
{
    public class HashFunctions
    {
        public static ulong HashString3(string path)
        {
            ulong i = 0;

            foreach (char c in path)
            {
                i = char.ToLower(c) + (16 * i);
                if ((i & 0xF0000000) != 0)
                    i ^= (i & 0xF0000000) ^ ((i & 0xF0000000) >> 24);
            }

            return i;
        }
        public static uint HashString2(string? str)
        {
            if (str is null)
            {
                return 0;
            }

            int hash = 0;
            foreach (char c in str)
            {
                hash = c - 'A' + 'a' + (65599 * hash);
            }
            return (uint)((65599 * hash) + 42);
        }
        public static uint HashStringSdbm(string section, string name)
        {
            return HashStringNorm(section + '*' + name);
        }

        public static uint HashStringNorm(string str)
        {
            uint hash = 0;

            for (var i = 0; i < str.Length; i++)
            {
                hash = char.ToLower(str[i]) + (65599 * hash);
            }

            return hash;
        }


        /// <summary>

        public static uint HashStringSDBMInc(ref uint hashVal, string cStr)
        {
            foreach (char c in cStr)
            {
                hashVal = (65599 * hashVal) + char.ToLower(c);
            }
            return hashVal;
        }
        //HashString 
        public static int HashStringSDBM(string cStr)
        {
            int hashVal = 0;
            if (cStr != null)
            {
                foreach (char c in cStr)
                {
                    hashVal = char.ToLower(c) + (65599 * hashVal);
                }
            }
            return hashVal;
        }

        public static int HashStringSDBMWithSeparator(string cStr, string separator)
        {
            int hashVal = HashStringSDBM(cStr);
            if (separator != null)
            {
                foreach (char c in separator)
                {
                    hashVal = char.ToLower(c) + (65599 * hashVal);
                }
            }
            return hashVal;
        }

        public static int HashStringSDBMWithAddStr(string cStr, string addStr)
        {
            int hashVal = HashStringSDBM(cStr);
            if (addStr != null)
            {
                foreach (char c in addStr)
                {
                    hashVal = char.ToLower(c) + (65599 * hashVal);
                }
            }
            return hashVal;
        }
        public static uint HashString10(string str)
        {
            uint hash = 0;
            uint temp = 0;

            foreach (char character in str)
            {
                hash = (hash << 4) + char.ToLower(character);
                temp = hash & 0xF0000000;

                if (temp != 0)
                {
                    hash = hash ^ (temp >> 24);
                    hash = hash ^ temp;
                }
            }

            return hash;
        }
        //HashString
        /*      public static int HashString(string name)
          {
              int hashVal = 0;
              foreach (char c in name)
              {
                  hashVal = char.ToLower(c) + 16 * hashVal;
                  if ((hashVal & 0xF0000000) != 0)
                  {
                      hashVal ^= (int)((hashVal & 0xF0000000) ^ ((hashVal & 0xF0000000) >> 24 & 0xFF));


                  }
              }
              return hashVal;
          }*/


        /* public static int HashString(string name)
         {
             long hashVal = 0;

             foreach (char c in name)
             {
                 // Convert character to lowercase using ASCII code
                 // Simulate _tolower in C++ using ASCII encoding
                 char lowerChar = (char)(c >= 'A' && c <= 'Z' ? c | 0x20 : c); // Convert uppercase to lowercase

                 // Calculate hash value
                 hashVal = lowerChar + 16 * hashVal;

                 // Apply reduction if necessary
                 if ((hashVal & 0xF0000000) != 0)
                 {
                     hashVal ^= (hashVal & 0xF0000000) ^ ((hashVal & 0xF0000000) >> 24);
                 }
             }

             return (int)hashVal;
         }*/

        public static uint HashString(string path)
        {
            // Convert the string to lowercase
            path = path.ToLower();

            uint hash = 0;
            const uint magic = 16;
            const uint mask = 0xF0000000;

            // Iterate over each character in the string
            for (int i = 0; i < path.Length; i++)
            {
                // Update the hash value
                hash = path[i] + (magic * hash);

                // Mask the high bits of the hash
                uint hm = hash & mask;
                if (hm > 0)
                {
                    // Apply the XOR operation with a shifted version of the mask
                    hash ^= hm ^ (hm >> 24);
                }
            }

            // Convert the final hash back to int
            return hash;
        }


        public static long HashString4(string name)
        {
            long hash = 0;

            foreach (char c in name.ToLower())
            {
                hash = c + (16 * hash);

                if ((hash & 0xF0000000) != 0)
                {
                    hash ^= (hash & 0xF0000000) ^ ((hash & 0xF0000000) >> 24);
                }
            }

            return hash;
        }
    }
}
