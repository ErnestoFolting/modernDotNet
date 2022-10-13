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
        [Fact]
        public void Add_AddKeyValuePair_AddedCorrectly()
        {
            KeyValuePair<int,string> pair = new KeyValuePair<int,string>(1, "a");
            
            _dict.Add(pair);

            Assert.Contains(1, _dict);
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
        [Fact]
        public void Remove_RemoveNotContainedElement_KeyNotFoundExceptionThrown()
        {
            Assert.Throws<KeyNotFoundException>(() => _dict.Remove(1));
        }
        [Fact]
        public void Remove_RemoveKeyValuePair_RemovedCorrectly()
        {
            KeyValuePair<int, string> pair = new KeyValuePair<int, string>(1, "a");
            _dict.Add(1, "a");

            _dict.Remove(pair);

            Assert.DoesNotContain(1, _dict);
        }
        [Fact]
        public void ContainsKey_CheckIfContainsNotContainedElement_False()
        {
            Assert.False(_dict.ContainsKey(1));
        }
        [Fact]
        public void ContainsKey_CheckIfContainsElementThatIsInChain_True()
        {
            myDict<string, string> dict = new myDict<string, string>()
            {
                {"1","a"},
                {"2","b"},
                {"3","c"},
            };
            Assert.True(dict.ContainsKey("1"));
        }
        [Fact]
        public void Clear_ClearAllElements_TheDictionaryIsEmpty()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            _dict.Clear();

            Assert.Empty(_dict);
        }
        [Fact]
        public void Count_CountTheElements_CorrectCount()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            Assert.Equal(3, _dict.Count);
        }

        [Fact]
        public void ThisByKeyGet_AccessToValueByKey_CorrectAccess()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            string value = _dict[2];

            Assert.Equal("b", value);
        }
        [Fact]
        public void ThisByKeyGet_AccessToNotExistingValueByKey_KeyNotFoundExceptionThrown()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            Assert.Throws<KeyNotFoundException>(() => _dict[4]);
        }
        [Fact]
        public void ThisByKeySet_ChangeTheValueOfElement_CorrectChange()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            _dict[2] = "check";

            Assert.Equal("check", _dict[2]);
        }
        [Fact]
        public void ThisByKeySet_TryToChangeValueOfElementThatNotExists_KeyNotFoundExceptionThrown()
        {
            Assert.Throws<KeyNotFoundException>(() => _dict[4]="check");
        }
        [Fact]
        public void ThisByKeySet_TryToChangeValueOfElementThatIsInChain_CorrectChange()
        {
            myDict<string, string> dict = new myDict<string, string>()
            {
                {"1","a"},
                {"2","b"},
                {"3","c"},
                {"4","d"},
            };

            dict["1"] = "check";

            Assert.Equal("check", dict["1"]);
        }
        [Fact]
        public void Values_GetValues_GotValuesIsCorrect()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            ICollection<string> lst = _dict.Values;
            ICollection<string> expected = new List<string>() { "a", "b", "c" };
            Assert.Equal(expected, lst);
        }
        [Fact]
        public void Keys_GetKeys_GotKeysIsCorrect()
        {
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            ICollection<int> lst = _dict.Keys;
            ICollection<int> expected = new List<int>() {1,2,3};
            Assert.Equal(expected, lst);
        }
        [Fact]
        public void IsReadOnly_CheckIfIsreadOnly_False()
        {
            Assert.False(_dict.IsReadOnly);
        }
        [Fact]
        public void TryGetValue_TryToGetExistingElement_GotElement()
        {
            myDict<string, string> dict = new myDict<string, string>()
            {
                {"1","a"},
                {"2","b"},
                {"3","c"},
            };
            string value = "";

            dict.TryGetValue("3", out value);

            Assert.Equal("c", value);
        }
        [Fact]
        public void TryGetValue_TryToGetElementThatNotExist_ReturnsDefault()
        {
            myDict<string, string> dict = new myDict<string, string>()
            {
                {"1","a"},
                {"2","b"},
                {"3","c"},
            };
            string value = "";

            dict.TryGetValue("4", out value);

            Assert.Equal(default(string), value);
        }
        [Fact]
        public void ContainsKeyValuePair_CheckIfContainsExistingElement_True()
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("1", "a");
            myDict<string, string> dict = new myDict<string, string>()
            {
                {"1","a"},
                {"2","b"},
                {"3","c"},
            };

            Assert.True(dict.Contains(pair));
        }
        [Fact]
        public void ContainsKeyValuePair_CheckIfContainsNotExistingElement_False()
        {
            KeyValuePair<string, string> pair = new KeyValuePair<string, string>("4", "d");
            myDict<string, string> dict = new myDict<string, string>()
            {
                {"1","a"},
                {"2","b"},
                {"3","c"},
            };

            Assert.False(dict.Contains(pair));
        }
        [Fact]
        public void CopyTo_TryToCopyMoreElementsThenCollectionSize_ArgumentExceptionThrown()
        {
            KeyValuePair<int, string>[] res = new KeyValuePair<int, string>[3];
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");
            _dict.Add(4, "d");
            Assert.Throws<ArgumentException>(() => _dict.CopyTo(res,0));
        }
        [Fact]
        public void CopyTo_TryToCopyToCollectionFromNegativeIndex_ArgumentExceptionThrown()
        {
            KeyValuePair<int, string>[] res = new KeyValuePair<int, string>[3];
            _dict.Add(1, "a");

            Assert.Throws<ArgumentException>(() => _dict.CopyTo(res, -1));
        }
        [Fact]
        public void CopyTo_TryToCopyElements_ElementsCopiedCorrectly()
        {
            KeyValuePair<int, string>[] res = new KeyValuePair<int, string>[3];
            _dict.Add(1, "a");
            _dict.Add(2, "b");
            _dict.Add(3, "c");

            _dict.CopyTo(res, 0);
            Assert.Equal(res,_dict);
        }
    }
}