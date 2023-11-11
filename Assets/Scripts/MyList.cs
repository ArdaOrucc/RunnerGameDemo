public class MyList<T>
{
    public static T[] arr;
    public static int Count;

    public MyList()
    {
        Count++;
    }

    public static void Add(T A)
    {
        arr[0] = A;
    }
}
