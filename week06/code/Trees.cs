public static class Trees
{
    /// <summary>
    /// Given a sorted list (sorted_list), create a balanced BST.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree();
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// Problem 5: Balanced insert of sorted values into BST.
    /// </summary>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        if (first > last)
            return;

        int mid = (first + last) / 2;
        int valueToInsert = sortedNumbers[mid];

        bst.Insert(valueToInsert);

        InsertMiddle(sortedNumbers, first, mid - 1, bst); // Left half
        InsertMiddle(sortedNumbers, mid + 1, last, bst);  // Right half
    }
}