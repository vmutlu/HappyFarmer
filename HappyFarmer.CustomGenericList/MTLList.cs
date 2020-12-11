using System.Collections;

namespace HappyFarmer.CustomGenericList
{
    /// <summary>
    /// Special List Of Veysel MUTLU Person
    /// </summary>
    public class MTLList
    {
        #region Fiels

        object[] _genericList;

        private const int _capasityValue = 10;

        public int _count = 0;

        public int Capacity { get { return _genericList.Length; } }

        #endregion

        #region Constructive Methods

        public MTLList()
        {
            _genericList = new object[_capasityValue];
        }

        public MTLList(int capasityValue)
        {
            _genericList = new object[capasityValue];
        }

        #endregion

        #region Methods

        public void Add(object addToData)
        {
            //If you want to add even though the capacity of the list is full, double the size of the list.
            if (_genericList.Length == _count)
            {
                object[] _listTwo = new object[_capasityValue * 2];
                for (int i = 0; i < _count; i++)
                    _listTwo[i] = _genericList[i];

                _genericList = _listTwo;
            }
            _genericList[_count] = addToData;
            _count++;
        }

        public void Remove(object removeToData)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_genericList[i] != null)
                    if (_genericList[i].Equals(removeToData))
                    {
                        for (int j = i + 1; j < _count; j++)
                            _genericList[j - 1] = _genericList[j];

                        _count--;
                        break;
                    }

                    else
                        break;
            }
        }

        public void Clear()
        {
            object[] _listThree = new object[_capasityValue];
            _genericList = _listThree;
            _count = 0;
        }

        public bool Search(object searchToData)
        {
            //Returns true if the value is found, otherwise false
            for (int i = 0; i < _count; i++)
            {
                if (_genericList[i] != null)
                    if (_genericList[i].Equals(searchToData))
                        return true;

                    else
                        break;
            }

            return false;
        }

        public void Reverse()
        {
            object[] _listToFour = new object[_count];
            for (int i = _count - 1, j = 0; i >= 0; i--, j++)
                _listToFour[j] = _genericList[i];

            for (int i = 0; i < _count; i++)
                _genericList[i] = _listToFour[i];
        }

        public void RemoveIndex(int indexNumber)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_genericList[i] != null)
                    if (i == indexNumber)
                    {
                        for (int k = i + 1; k < _count; k++)
                            _genericList[k - 1] = _genericList[k];


                        _count--;
                        break;
                    }
                    else
                        break;
            }
        }

        public int IndexOf(object indexSearchData)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_genericList[i] != null)
                {
                    if (_genericList[i].Equals(indexSearchData))
                        return i; //index of data
                }
                else
                    break;
            }
            return 0; //No Fount Data
        }

        public IEnumerator GetEnumerator()
        {
            return _genericList.GetEnumerator();
        }

        public object this[int index]
        {
            get
            {
                return _genericList[index];
            }
            set
            {
                _genericList[index] = (object)value;
            }
        }

        #endregion
    }
}
