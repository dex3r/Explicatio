/**
 * Overall guidelines:
 * 1. Write everything in English. Especially something, that anybody can see.
 * 2. Comment everything you can that is non-trivial, especially public properties/methods.
 * 3. Do not ever use default accessor.
 * 4. Always do code auto-format (Ctrl+E, D; Edit -> Advanced -> Format document) before commit.
 * 5. Don't use "using organizing".
 * 6. Try not to use regions where not necessary.
 * 7. If something is not specified here follow http://msdn.microsoft.com/en-us/library/ff926074.aspx
 * and http://msdn.microsoft.com/en-us/library/ms229002(v=vs.110).aspx
 * 
 * Recommended software:
 * 1. Visual Studio 2012-2013.
 * 2. Plugin CommentsPlus.
 * 3. Plugin NuGet.
 * 4. Plugin NShader (VS 2013 versio: http://www.jostavo.net/NShader.rar).
 * 5. Visual Studio Spell Checker.
 */

/**
 * Usings in the following order:
 * 1. System.
 * 2. Microsoft.
 * 3. OpenTK.
 * 4. Other third-party libraries.
 * 5. Explicatio.
 */
using System;

// Use correct, logical namespaces
namespace Explicatio.Main
{
    /**
     * Classes, etc:
     * 1. One public class per file.
     * 2. When non-public/internal class is longer than just few lines put it in another file.
     * 3. Use static classes where instancing, inheritance and singleton are not necessary.
     * 4. If your non-static class have static content, separate the static region (non-static first).
     * 5. Class name must be identical with the file name.
     * 6. Use 'I' and 'E' prefix for Interface and Enum accordingly.
     */
    public class CodeExample
    //public static class CodeExample
    //public interface ICodeExample
    //public enum ECodeExample
    {
        //? Use this only for classes with static content
        #region STATIC

        /**
         * Fields:
         * 1. Never make public/internal/default fields.
         * 2. Use auto-implemented properties if both of them should be default.
         * 3. Properties always should have getter and (private)setter -> don't use field if there is property for it (except for some special cases).
         * 4. Never use "var" and "dynamic".
         */

        /// <summary>
        /// Use summary for commenting fields that doesn't have a property
        /// </summary>
        public static int SomeInt { get; set; }
        public static float SomeFloat { get; private set; }
        // Use string instead String (and int rather than Int32 etc.) where possible
        private static float someHealth;

        // Divide entirely private fields by one blank line (like above)
        private static int somePrivateInt;

        /* Properties:
         * 1. Use properties auto-generation (Encapsulate Field; Ctrl+R, Ctrl+E).
         * 2. If a property is not default always put brackets in new lines.
         * 3. Always use this properties section example (repeated in the end of the file) if there are any.
         */
        //!? Static properties region
        #region PROPERTIES

        public static string SomeHealth
        {
            /// <summary>
            /// Use summary for property (if exist) and not for field.
            /// </summary>
            get { return someString; }
            set
            {
                someHealth = value;
                if (someHealth < 0)
                {
                    someHealth = 0;
                }
            }
        }

        #endregion
        //!? END of static properties region

        static CodeExample()
        {
            // Initialize static variables in the static constructor if needed (not default)
            SomeInt = 2;
            SomeHealth = 10;
            somePrivateInt = 5;
        }

        #endregion

        public float NiceFloat { get; set; }
        private int someValue;

        private int someNonStaticField;

        //!? Properties region
        #region PROPERTIES

        public byte SomeValue
        {
            get { return someValue; }
            set
            {
                someValue = value;
                if (someValue < 0)
                {
                    someValue = 0;
                }
            }
        }

        #endregion
        //!? END of properties region

        public CodeExample()
        {
            SomeNonStaticField = 0;
        }

        public CodeExample(string someString) : this()
        {
            this.someString = someString;
        }

        // Use PascalCase for public/internal methods...
        public void DoSomething()
        {
            // Always use brackets for statements, even if they have only one line body
            if (someValue > 0)
            {
                doSomethingElse();
            }
        }

        // ...and camelCasing for private/protected
        private void doSomethingElse()
        {

        }

        /// <summary>
        /// Use summary for methods
        /// </summary>
        /// <param name="someParamater"></param>
        /// <returns></returns>
        public int ReturnSomething(int someParamater)
        {
            return someParameter * 2;
        }
    }
}

// Recommendation: put these two in your Toolbox

        //!? Properties region
        #region PROPERTIES

        #endregion
        //!? END of properties region


        //!? Static properties region
        #region PROPERTIES

        #endregion
        //!? END of static properties region