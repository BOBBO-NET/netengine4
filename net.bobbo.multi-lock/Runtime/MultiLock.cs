using System.Collections;
using System.Collections.Generic;

namespace BobboNet
{
    // Class that allows for something to be locked & unlocked.
    //      If there are any keys at all on this lock, that means it will be considered locked.
    //      If there are ZERO keys, then it will be considered unlocked.
    //
    // This is so multiple things can have control of locking and unlocking various state elements, but not collide.
    // For example, locking if the player can move or not! Various elements may want to control this, like
    // the pause menu, a cutscene, or a game mechanic!
    public class MultiLock
    {
        // The internal datastructure that holds currently locked keys
        private Dictionary<string, bool> lockDict = new Dictionary<string, bool>();

        // Adds a lock key to this MultiLock, making it considered as locked. If lockKey is already in this lock, then it will return false. Otherwise it will return true.
        public bool Lock(string lockKey)
        {
            if (lockDict.ContainsKey(lockKey))
            {
                return false;
            }
            else
            {
                lockDict.Add(lockKey, false);
                return true;
            }
        }

        // Removes a lock key from this MultiLock. If lockKey isn't in this lock, then it will return false. Otherwise it will return true.
        public bool Unlock(string lockKey)
        {
            return lockDict.Remove(lockKey);
        }

        // Removes ALL LOCK KEYS from this MultiLock, making it considered as unlocked.
        public void ClearLocks()
        {
            lockDict.Clear();
        }

        // Returns true if this MultiLock has ANY lock keys on it, false if there are none.
        public bool IsLocked()
        {
            return lockDict.Count != 0;
        }
    }
}