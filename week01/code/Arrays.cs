public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN:
        // Step 1: Create a new array of type double with size 'length' to store the multiples
        // Step 2: Loop through each index position in the array from 0 to length-1
        // Step 3: For each position i, calculate the multiple by multiplying number by (i+1)
        //         - We use (i+1) because array indices start at 0, but we want the 1st, 2nd, 3rd multiples
        //         - Example: For number=7, position 0 should be 7*1=7, position 1 should be 7*2=14, etc.
        // Step 4: Store the calculated multiple in the array at index i
        // Step 5: After the loop completes, return the filled array

        // Implementing the plan:

        // Step 1: Create array to hold the multiples
        double[] multiples = new double[length];

        // Step 2 & 3 & 4: Loop through and calculate each multiple

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        // Step 5: // Step 5: Return the completed array




        return multiples; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.


        // PLAN:
        // Step 1: Determine the split point where we divide the list
        //         - When rotating right by 'amount', the last 'amount' elements move to the front
        //         - The split point is at index: data.Count - amount
        //         - Example: [1,2,3,4,5,6,7,8,9] rotated right by 3
        //           Split at index 9-3=6, giving us [1,2,3,4,5,6] and [7,8,9]
        // Step 2: Extract the last 'amount' elements (the part that rotates to the front)
        //         - Use GetRange(startIndex, count) to get elements from split point to end
        // Step 3: Extract the first part (everything before the split point)
        //         - Use GetRange(0, splitPoint) to get elements from beginning to split
        // Step 4: Clear the original list to prepare for reorganization
        // Step 5: Add the rotated part first (the elements that were at the end)
        //         - Use AddRange() to add the last part
        // Step 6: Add the first part after it (the elements that were at the beginning)
        //         - Use AddRange() to add the first part
        // Result: The list is now rotated right by the specified amount

        // Implementing the plan:
        // Step 1: Determine the split point
        int splitPoint = data.Count - amount;

        // Step 2: Extract the last 'amount' elements
        // GetRange(startIndex, count) - start at splitPoint, take 'amount' elements

        List<int> lastpart = data.GetRange(splitPoint, amount);

        //Step 3: Get the first part (everything before the split point)
        // GetRange(0, splitPoint) - start at 0, take 'splitPoint' elements
        List<int> firstpart = data.GetRange(0, splitPoint);

        // Step 4: Clear the original list
        data.Clear();

        // Step 5: Add the last part first (this is the rotation)
        data.AddRange(lastpart);

        // Step 6: Add the first part after it
        data.AddRange(firstpart);

         // The list 'data' is now rotated right by 'amount' positions

    }
}
