using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class asyncfunction : MonoBehaviour
{
    //Ejecutandolo en async pero si el wait Task se utiliza de forma normal
    async void Start() //Puede ser publico, privado, async, void, no void, static
    {
        await taskTest2(); //Metodo task
        await Task.Run ( function1); //Task.Run y la funcion, la convierte en una tarea
        Debug.Log("Función Sincrona terminada");
        await taskTest3(7);
    }

   
    void Update()
    {
        
    }
    #region Teoria
    //-------------------Introduccion----------------
    //Para manejar tareas concurrentes de forma elegante
    //Para ejecutar tareas que puedan tardar (Leer archivos, acceder a BBDD) Sin bloquear el hilo principal
    //Duran distinto en cada ordenador, no se sabe cuantos ciclos de update puede hacer
    //En teoria es mas eficiente
    //poner awake Task... Es lo mismo que hacer una funcion Task 

    //--------------Requisitos---------------
    //Necesitan una condicion de espera, sino se comporta como una funcion normal
    //Funcion async debe retornar:
    //      Task(funciones que devuelven algo),
    //      Task<T> (devuelven valores int, string...)
    //      void (Solo para eventos)

    //-------------------Ventajas y Desventajas----------------
    //+ Rendimiento = Espera a que terminen las otras tareas (No va en el mismo bucle de espera que el Update)
    //+ Rendimiento  = Diseñadas para no saturar los metodos normales. 
    //- Mas dificil de depurar
    //Buen control de errores 

    //-------------------Casos de usos----------
    //tareas que puedan tardar
    //Cargar recursos en segundo plano
    //Usos : Leer archivos, acceder a BBDD, Accesos a web, a una API ,

    //------------------Errores-------------
    //QUE HAGA SOLO UNA TAREA, no poner muchas funciones dentro del async
    //Si hay bucles infinitos complica las Async, pero se puede poner un condicionante de tiempo
    //No afecta al resto del tiempo
    //Errores: No usar await (la funcion se lanza pero no se espera)
    //Usar async void en lugar de async Task : no se puede manejar errores
    //Olvidar ConfigureAwait(false) en librerias si no necesitas el contexto 
    

    //---------------Preguntas---------
    //¿Cuando se necesita await.Task y cuando return.Task. MIRAR EL CASO 6 
    //       Return es cuando esperas un valor
    //      Necesitas Task para las funciones que usan await, para dar un valor
    //      await si esperas que la tarea termine antes de seguir
    //      return Task si la otra funcion espera a que tu termines 
    //Diferencia public Task ___() y public async Task ___()
    //      Public async Task Metodo seguro (Puede usarse con await y control de errores
    //      Public async void MetodoPeligroso(), solo en eventos pero no hay control de errores 

    //--------Task.--------
    //await Task.CompletedTask;   Espera a que terminen las demas 
    //await Task.Delay(1000); //Recibe el tiempo en milisegundos. 1000 = 1seg 
    //await Task.FromResult  Es una tarea que ha terminado con un valor, util en testing cuando ya tienes el resultado
    //await Task.Load
    //await Task.LoadAsync //ej, Para cuando tienes assets que no hace falta cargar al principio, lo cargas mas adelante como en la prox escena
    //await Task.WhenAll(Func1(), Func2(), Func3());
    #endregion
    #region 1 Como son
    public async void function1() //Esperar para que te de la respuesta
    {
        Debug.Log("Funcion Task ejecutada");
        await Task.CompletedTask; //Espera a que las demas terminen
        Debug.Log("Funcion Task terminada");
        await Task.Delay(1000); //Recibe el tiempo en milisegundos. 1000 = 1seg 
        Debug.Log("Funcion Task Delay terminada");
    }
    #endregion

    #region 2 Async / Task vs Corrutina  Iguales, pero la corrutina es mas facil de parar StopCoroutine
    public async void function2()
    {
        await taskTest2(); //Task
        Test2(); //Normal tambn se pueden usar
        await Task.CompletedTask;
    }
    public Task taskTest2()
    {
        Debug.Log("Funcion Task ");
        return Task.CompletedTask; //Como un yield return null, que lo necesita 

    }
    public void Test2()
    {
        Debug.Log("Funcion normal");
    }
    IEnumerator corrutina1() //Mas usado para gestionar valores de la escena
    {
        Debug.Log("Corrutina ejecutada");
        yield return new WaitForSeconds(1); //Igual a la asincrona 
    }
    #endregion

    #region 3 Valores 
    public Task taskTest3(int valor)
    {
        Debug.Log("Funcion 3 con dato: " + valor);
        return Task.CompletedTask;
    }
    public async Task taskTestAsync3()
    {
        Debug.Log("Async Task");
        await Task.CompletedTask; 
    }
    public async Task<float> tasktTest3(int valor)
    {
        Debug.Log("Funcion 3.1 con dato" + valor);
        await Task.Delay(1000);
        return valor / 2f;
        
    }


    #endregion

    #region 4 Bucles
    public async void funcionBucle4() 
    {
        while (true)
        {

        } 

        await Task.CompletedTask; //Si esta fuera dice que nunca va a poder hacerlo por ser un bucle infinito
    }
    public async void funcionBucle41()
    {
        int counter = 0;
        while (true)
        {
            counter++;
            if (counter > 1000)
                break;
        }

        await Task.CompletedTask; //No bucle infinito
        Debug.Log("Bucle terminado");
    }


    #endregion

    #region 5 Ejemplo de cargar info de clientes
    public async Task<string> GetDataAsync()
    {
        HttpClient client = new HttpClient();
        string result = await client.GetStringAsync("https://api.example.com/data");
        return result;
    }
    #endregion

    #region 6 await Task y return Task 
    public async Task MiFuncion()
    {
        await Task.Delay(1000); // Espera aquí
    }
    public Task MiFuncion2()
    {
        return Task.Delay(1000); // Devuelve la tarea a otra función
    }
    #endregion
}
