using System;
using System.Collections.Generic;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.
    /// </summary>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // Step 1: Create a new array of size 'length'
        // Step 2: Use a loop to fill it with multiples: number * (i + 1)
        // Step 3: Return the array

        double[] multiples = new double[length];
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }
        return multiples;
    }

    /// <summary>
    /// Rotates the list to the right by the given amount. Modifies the list in-place.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // Step 1: Normalize the amount using modulo
        // Step 2: Slice last 'amount' items (to be moved to front)
        // Step 3: Slice first part (the rest)
        // Step 4: Clear original list and re-add in rotated order

        amount = amount % data.Count;

        List<int> endSlice = data.GetRange(data.Count - amount, amount);
        List<int> startSlice = data.GetRange(0, data.Count - amount);

        data.Clear();
        data.AddRange(endSlice);
        data.AddRange(startSlice);
    }
}