public class SingletonCounter
{
    private static SingletonCounter? instance = null;
    private int activeUsers = 0; // Contador de usuarios activos
    private static readonly object lockObject = new object(); // Para evitar problemas en entornos multihilo

    private SingletonCounter()
    {
    }

    public static SingletonCounter GetInstance()
    {
        // Bloqueo para que solo un hilo a la vez pueda crear la instancia
        lock (lockObject)
        {
            if (instance == null)
            {
                instance = new SingletonCounter();
            }
        }
        return instance;
    }

    // Método para incrementar el contador cuando un usuario hace login
    public int UserLoggedIn()
    {
        lock (lockObject)
        {
            activeUsers++;
            return activeUsers;
        }
    }

    // Método para decrementar el contador cuando un usuario hace logout
    public int UserLoggedOut()
    {
        lock (lockObject)
        {
            if (activeUsers > 0)
                activeUsers--;
            return activeUsers;
        }
    }

    // Obtener la cantidad actual de usuarios activos
    public int GetActiveUsers()
    {
        return activeUsers;
    }
}

