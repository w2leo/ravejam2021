using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Characters
{
    public string name { get; protected set; }
    private float x, y; 
    private int HP; 
    private int maxHP;
}

public class Robot : Characters
{
    public Robot(string name)
    {
        this.name = name;
    }
    public void GoHunt(Prisioner prisioner)
    {
        Debug.Log("Go to hunt for " + prisioner.name);
    }
    public void WaitUntilNeedHunt()
    {
        Debug.Log("I'm waiting for hunting");                                                                                                                                               
    }
}
public class Prisioner : Characters
{
    public Prisioner(string name, int age, int prisonPeriod)
    {
        this.name = name;
        this.age = age;
        //datePrisoned = Today();
        prisonTime = 0;
        prisonExpirience = 0;
        prisonEnlightment = 0;
        this.prisonPeriod = prisonPeriod;
    }

    private int age;    
    private int datePrisoned; //Начало заключения
    private int prisonPeriod; //Срок заключения
    private int prisonTime; // Дней в заключении. Добавить авто увеличение
    private int prisonExpirience; // Опыт. Добавить увеличение от работы
    private int prisonEnlightment; //Озарение? Тоже как-то изменяется

    public void DoWhatYouWant()
    {
        Debug.Log($"{name} {age} is doing what he want");
    }
}

public class Animal : Characters
{
    public void WalkWhileAlive()
    {
        Debug.Log("I'm living here");
    }
}
