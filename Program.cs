//Необходимо разработать платформу для создания и выполнения различных задач с использованием делегатов. 

//    Платформа должна поддерживать динамическое добавление различных типов задач, таких как вычислительные задачи, задачи ввода/вывода и задачи логирования. 
//    Каждая задача должна быть независимой и настраиваемой, с возможностью добавления новых типов задач без изменения основного кода системы.

//Требования
//Создайте интерфейс ITask с методом Execute, который принимает параметр типа TaskContext.

//Создайте класс TaskContext, который будет содержать информацию о текущем состоянии задачи и будет передаваться в метод Execute.

//Создайте делегат TaskHandler, который будет представлять метод выполнения задачи и принимать параметр типа TaskContext.

//Создайте класс TaskProcessor, который будет содержать цепочку задач.

//В классе TaskProcessor добавьте метод AddTask, который будет принимать делегат TaskHandler и добавлять его в цепочку задач.

//Создайте метод Run в классе TaskProcessor, который будет вызывать делегаты в цепочке задач в порядке их добавления.

//Реализуйте несколько классов задач:

//ComputationTask - вычислительная задача.
//IOTask - задача ввода / вывода.
//LoggingTask - задача логирования.
//Напишите тестовую программу, которая будет создавать задачи и передавать их в TaskProcessor для выполнения. Подпишите методы классов задач на соответствующие этапы выполнения.

using System.Reflection.Metadata;

ComputationTask task1 = new ComputationTask("sum");
IOTask task2 = new IOTask("vvod");
LoggingTask task3 = new LoggingTask("log");


TaskProcessor processor = new TaskProcessor();

processor.AddTask(task1.Execute);
processor.AddTask(task2.Execute);
processor.AddTask(task3.Execute);


processor.Run();

Console.ReadLine();




interface ITask
{
    void Execute(TaskContext obj);
}

public class TaskContext
{
    public string Status; //содержать информацию о текущем состоянии задачи
    public TaskContext( string status)
    {
    
        Status = status;
    }
}




public delegate void TaskHandle(TaskContext obj);

public class TaskProcessor
{
    List<TaskHandle> taskHandles = new List<TaskHandle>();

    public void AddTask(TaskHandle task)
    {
        taskHandles.Add(task);
    }

    public void Run()
    {
        foreach (var task in taskHandles)
        {
            var methodInfo = task.Method;
            TaskContext context = new TaskContext( "выполняется");
            TaskContext context2 = new TaskContext("выполнена");

            task.Invoke(context);
            Console.WriteLine($"задача {methodInfo.DeclaringType.Name}, статус: {context.Status}");
            
            Console.WriteLine($"задача {methodInfo.DeclaringType.Name}, статус: {context2.Status}");
        }
    }
}

public class ComputationTask : ITask
{
    public string Name { get; set; }
    public int Parametr1;
    public int Parametr2;
    public ComputationTask(string name)
    {
        Name = name;
       
    }
   
    public void Execute(TaskContext obj)
    {
        Console.WriteLine($"вычислительная задача"); 
    }
}


public class IOTask : ITask
{
    public string Name;
    public string str;

    public IOTask(string name)
    {
        Name = name;
    }
    public void Execute(TaskContext obj)
    {
        Console.WriteLine("задача ввода/вывода");
    }
}


public class LoggingTask : ITask
{
    public string Name;
    public LoggingTask(string name)
    {
        Name = name;
    }
    public void Execute(TaskContext obj)
    {
        Console.WriteLine("задача логирования");
    }
}
