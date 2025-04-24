using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class asyncfunction : MonoBehaviour
{
    //Ejecutandolo en async pero si el wait Task se utiliza de forma normal
    async void Start() //Puede ser publico, privado, async, void, no void
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
    //Espera a que terminen las otras tareas (No va en el mismo bucle de espera que el Update)
    //Diseñadas para no saturar los metodos normales. 
    //Necesitan una condicion de espera, sino se comporta como una funcion normal
    //Duran distinto en cada ordenador, no se sabe cuantos ciclos de update puede hacer
    //En teoria es mas eficiente
    //poner awake Task... Es lo mismo que hacer una funcion Task 

    //-------------------Casos de usos----------
    //Usos : Accesos a web, a una API ,

    //------------------Errores-------------
    //QUE HAGA SOLO UNA TAREA, no poner muchas funciones dentro del async
    //Si hay bucles infinitos complica las Async, pero se puede poner un condicionante de tiempo
    //No afecta al resto del tiempo

    //---------------Preguntas---------
    //¿Cuando se necesita await.Task y cuando return.Task
    //Diferencia public Task ___() y public async Task ___()
    //--------Task.
    //await Task.CompletedTask;   Espera a que terminen las demas 
    //await Task.Delay(1000); //Recibe el tiempo en milisegundos. 1000 = 1seg 
    //Task.FromResult
    //Task.Load
    //Task.LoadAsync //ej, Para cuando tienes assets que no hace falta cargar al principio, lo cargas mas adelante como en la prox escena
    #endregion
    #region 1
    public async void function1() //Esperar para que te de la respuesta
    {
        Debug.Log("Funcion Task ejecutada");
        await Task.CompletedTask; //Espera a que las demas terminen
        Debug.Log("Funcion Task terminada");
        await Task.Delay(1000); //Recibe el tiempo en milisegundos. 1000 = 1seg 
        Debug.Log("Funcion Task Delay terminada");
    }
    #endregion
    #region 2
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
    #endregion

    IEnumerator corrutina1() //Mas usado para gestionar valores de la escena
    {
        Debug.Log("Corrutina ejecutada");
        yield return new WaitForSeconds(1); //Igual a la asincrona 
    }

    #region 3
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
}
