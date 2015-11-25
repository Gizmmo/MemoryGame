public class ArrayHelpers {

	/// <summary>
    /// Returns a random int array between 0 and the size passed.
    /// </summary>
    /// <returns>A int array with random numbers as elements</returns>
    public static int[] GetRandomIntArray(int size) {
        return GetRandomIntArray(size, size);
    }

    /// <summary>
    /// Returns a random int array between 0 and the max passed.
    /// </summary>
    /// <returns>A int array with random numbers as elements</returns>
    public static int[] GetRandomIntArray(int size, int max) {
        
        // Create a new int array of the max amount passed
        var fullArray = new int[max];
        // Create a new int array of the size passed
        var returnArray = new int[size];

        // For each element in the int array...
        for (var i = 0; i < max; i++) {
            // ...put the number of the index in the array position
            fullArray[i] = i;
        }

        // Shuffle the fullArray.
        fullArray.ShuffleArray();

        // For each element in the returnArray...
        for (var i = 0; i < size; i++) {
            // ...grab the corresponding index of the randomized array.
            returnArray[i] = fullArray[i];
        }

        // Return the array Shuffled with only the passed size.
        return returnArray;
    }

}
