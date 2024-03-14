using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Space3x.Core.VirtualEntities
{
    public enum DestroyObjectType {
        DestroyNone,
        DestroyComponent,
        DestroyGameObject
    }
    
    public class VirtualEntity<T> where T : class //, IVirtualEntity
    {
        private List<T> _values = new List<T>();

        /// <summary>
        /// Returns the number of items in this VirtualEntity.
        /// </summary>
        public int Count => _values.Count;

        /// <summary>
        /// Returns the first item sharing its Type with <typeparamref name="T2"/>
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public T Get<T2>() => _values.OfType<T2>().FirstOrDefault() as T;

        /// <summary>
        /// Returns all items sharing their Type with <typeparamref name="T2"/>
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T2>() => _values.Where(v => v is T2);
        
        /// <summary>
        /// Same as <see cref="GetAll{T2}"/> but returns items of type <typeparamref name="T2"/>
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public IEnumerable<T2> OfType<T2>() => _values.OfType<T2>();

        /// <summary>
        /// Returns all items sharing their Type with both <typeparamref name="T2"/>
        /// and <typeparamref name="T3"/>, as <c>IEnumerable&lt;T3&gt;</c>.
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public IEnumerable<T3> OfType<T2, T3>() => _values.OfType<T2>().OfType<T3>();
        
        /// <summary>
        /// Removes supplied value from the list.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(T value)
        {
            return _values.Remove(value);
        }
        
        /// <summary>
        /// Removes item at index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="destroyObjectType"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveAt(int index, DestroyObjectType destroyObjectType = DestroyObjectType.DestroyComponent)
        {
            if (_values[index] is Component component)
            {
                switch (destroyObjectType)
                {
                    case DestroyObjectType.DestroyGameObject:
                        _values.RemoveAt(index);
                        if (Application.IsPlaying(component.gameObject))
                        {
                            GameObject.Destroy(component.gameObject);
                        }
                        else
                        {
                            GameObject.DestroyImmediate(component.gameObject, false);
                        }
                        break;
                    case DestroyObjectType.DestroyComponent:
                        _values.RemoveAt(index);
                        Component.Destroy(component);
                        break;
                    case DestroyObjectType.DestroyNone:
                        _values.RemoveAt(index);
                        break;
                    default:
                        throw new NotImplementedException(
                            $"Destroy Object Type {destroyObjectType} not implemented.");
                }
            }
            else
            {
                _values.RemoveAt(index);
            }
        }
        
        /// <summary>
        /// Removes all items sharing their Type with <typeparamref name="T2"/>
        /// </summary>
        /// <param name="destroyObjectType"></param>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public int RemoveAll<T2>(DestroyObjectType destroyObjectType = DestroyObjectType.DestroyComponent)
        {
            var removeCount = 0;
            for (var i = _values.Count - 1; i >= 0; i--)
            {
                if (_values[i] is T2 match)
                {
                    RemoveAt(i, destroyObjectType);
                    removeCount++;
                }
            }

            return removeCount;
        }

        /// <summary>
        /// Adds supplied value to the list, returning true if it didn't already exist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if it's newly added, false otherwise.</returns>
        public bool Add(T value)
        {
            switch (_values.Any(eachValue => eachValue == value))
            {
                case false:
                    _values.Add(value);
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Adds supplied value to the list only if there's no other list item matching T2, returning true.
        /// Otherwise, false.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public bool Add<T2>(T value) where T2 : class
        {
            switch (_values.Any(eachValue => eachValue is T2))
            {
                case false:
                    _values.Add(value);
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Adds supplied value to the list only if there's no other list item matching T2, returning true.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destroyObjectType"></param>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public bool AddOrReplace<T2>(T value, DestroyObjectType destroyObjectType = DestroyObjectType.DestroyComponent)
        {
            var isMatch = false;
            for (var i = _values.Count - 1; i >= 0; i--)
            {
                if (_values[i] is T2 match)
                {
                    isMatch = true;
                    if (_values[i] == value) return true;
                    RemoveAt(i, destroyObjectType);
                    _values.Insert(i, value);
                    break;
                }
            }
            if (!isMatch) _values.Add(value);
            return isMatch;
        }

        /// <summary>
        /// Removes all items in the list matching T2 and adds value as new item unless it was already
        /// existing, keepìng the last occurrence in the list in such case. Returns true if newly added.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destroyObjectType"></param>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public bool RemoveAllAndAdd<T2>(T value, DestroyObjectType destroyObjectType = DestroyObjectType.DestroyComponent)
        {
            var isMatch = false;
            for (var i = _values.Count - 1; i >= 0; i--)
            {
                if (_values[i] is T2 match)
                {
                    if (_values[i] == value)
                    {
                        if (!isMatch)
                        {
                            isMatch = true;
                        }
                        else
                        {
                            _values.RemoveAt(i);
                        }
                    }
                    else
                    {
                        RemoveAt(i, destroyObjectType);
                    }
                }
            }

            if (!isMatch) _values.Add(value);
            return !isMatch;
        }

        /// <summary>
        /// Add overload to support the new implementation while keeping backwards compatibility.
        /// </summary>
        public bool Add(IEntityModule value) => Add(value as T);

        /// <summary>
        /// Remove overload to support the new implementation while keeping backwards compatibility.
        /// </summary>
        public bool Remove(IEntityModule value) => Remove(value as T);
    }
}
