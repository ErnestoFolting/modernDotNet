using Xunit;
using System;
using System.Collections.Generic;

namespace myDictionaryLib.Tests
{
    public class UnitTest1
    {
        private myDict<int, string> _dict;
        public UnitTest1() => _dict = new myDict<int, string>();
        [Fact]
        public void ConstructorWithEnumerableCollection_InitializeMyDictionaryWithElements_InitializedCorrectly()
        {
            var expected = new Dictionary<int, string>()
            {
                {1,"a"},
                {2,"b"},
                {3,"c"},
            };

            myDict<int, string> dict = new myDict<int, string>()
            {
                {1,"a"},
                {2,"b"},
                {3,"c"},
            };

            Assert.Equal(expected, dict);
        }
        [Fact]
        public void Add_AddElementToDictionary_ElementAddedCorrectly()
        {
            Dictionary<int, string> standartDictionary = new Dictionary<int, string>() {
                { 1,"a"},
                { 2,"b"},
            };
            
            _dict.Add(1, "a");
            _dict.Add(2, "b");

            Assert.Equal(standartDictionary, _dict);
        }
        [Fact]
        public void Add_AddAfterRemove_ElementAddedOnTheRemovedElementPosition()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");
            _dict.Remove(2);
            Dictionary<int, string> standartDictionary = new Dictionary<int, string>() {
                { 1,"a"},
                { 4,"d"},
                { 3,"c"},
            };

            _dict.Add(4, "d");

            Assert.Equal(standartDictionary, _dict);

        }
        [Fact]
        public void Add_AddToFullDictionary_DictionaryResizeAndElementAdd()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            _dict.Add(4, "d");
            Assert.Contains(4, _dict);
        }
        [Fact]
        public void Add_AddNullElement_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => _dict.Add(1, null));
        }
        [Fact]
        public void Add_AddDuplicateKey_InvalidOperationExceptionThrown()
        {
            _dict.Add(1, "a");
            Assert.Throws<InvalidOperationException>(() => _dict.Add(1, "a"));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]

        public void Remove_RemoveElement_ElementRemovedCorrectly(string keyToRemove)
        {
            myDict<string, string> dict = new myDict<string, string>()
            {
                {"1","a"},
                {"2","b"},
                {"3","c"},
            };
            dict.Remove(keyToRemove);
            Assert.DoesNotContain(keyToRemove,dict);
        }
    }
}