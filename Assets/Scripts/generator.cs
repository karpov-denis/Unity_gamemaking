using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour {
	public void checkSide(int[,] field,int i,int j,ref int close)
	{
		try{
		if(field[i,j]==1)
			close++;
		if(field[i,j]==3)
			close+=10;
		if(field[i,j]==2)
			close+=100;
		}
		catch(Exception E){

		}

	}
	public int TekType()
	{
		return field[tekY,tekX];
	}
	public void LogTekPlate()
	{
		switch(TekType())
		{
			case -1 : Debug.Log("HOW YOU CAME HERE");break;
			case 0 : Debug.Log("HOW YOU CAME HERE2");break;
			case 3 : Debug.Log("it is simple tile");break;
			case 2 : Debug.Log("it is enemy tile");break;
			case 1 : Debug.Log("it is treasure tile");break;
		}
	}
	public GameObject current()
	{
		return fieldObj[tekX,tekY];
	}
	public GameObject current(int i,int j)
	{
		return fieldObj[i,j];
	}

	public bool checkPlate(int j,int i)
	{
		try{
		if(field[i,j]>0)
		return true;
		else
		{
			return false;
		}
		}
		catch(Exception E)
		{
			return false;
		}
	}

	public bool SetPlayer(int i,int j)
	{
		if(checkPlate(i,j))
		{
		Player=Instantiate(PlayerPfb,new Vector3(0,0.5F,0),Quaternion.identity);
		Debug.Log("Character instanted on :"+i+" " + j);
		tekX=0;
		tekY=0;
		return true;
		}
		else
		{
			Debug.Log("Failed to instante character on :"+i+" " + j);
			return false;
		}
	}
	public bool MovePlayer(int deltaI,int deltaJ)
	{
		if(checkPlate(tekX+deltaI,tekY+deltaJ))
		{
			Player.transform.position = new Vector3( Player.transform.position.x+deltaI, Player.transform.position.y, Player.transform.position.z+deltaJ);
			tekX+=deltaI;
			tekY+=deltaJ;
			Debug.Log("Character moved to :"+(tekX)+" " + (tekY));
			LogTekPlate();
			return true;
		}
		else
		{
			Debug.Log("Failed to move character to :"+(tekX+deltaI)+" " + (tekY+deltaJ));
			return false;
		}
	}

    private int CheckConnections()
    {
        int a = 1; 
        for(int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                a *= Checkfield[i, j];
            }
        }
        return a;
    }

    private void GenerateField()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                field[i, j] = 0;    //-1&&0-empty 1-simple 2-enemy 3-treasure
            }
        }
        field[0, 0] = 1;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (field[i, j] == 0)
                {
                    int close = 0;
                    checkSide(field, i - 1, j, ref close);
                    checkSide(field, i + 1, j, ref close);
                    checkSide(field, i, j + 1, ref close);
                    checkSide(field, i, j + 1, ref close);
                    field[i, j] = (close + rnd.Next()) % 4;
                    if (field[i, j] == 0)
                        field[i, j] = -1;
                }

            }
        }
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (field[i, j] == 0 || field[i, j] == -1)
                {
                    Checkfield[i, j] = -1;
                }
                else
                {
                    Checkfield[i, j] = 0;
                }
            }
        }
        Checkfield[0, 0] = 1;
    }

    public void CheckMazeTile(ref int[,] a, int i, int j)
    {
        try
        {
            if (a[i, j] == 0)
            {
                a[i, j] = 1;
                CompleteMaze(ref a, i, j);
            }
        }
        catch
        {

        }
    }

    public void CompleteMaze(ref int[,] a,int i,int j)
    {
        CheckMazeTile(ref a, i, j - 1);
        CheckMazeTile(ref a, i, j + 1);
        CheckMazeTile(ref a, i-1, j);
        CheckMazeTile(ref a, i+1, j);
    }

	public GameObject PlayerPfb;
	public GameObject Player=null;
	public int tekX=0;
	public int tekY=0;
	public GameObject SimplePlate;
	public GameObject EnemyPlate;
	public GameObject TreasurePlate;
	public GameObject Plate;
	public static int size = 5;
	public int[,] field = new int[size,size];
    public int[,] Checkfield = new int[size, size];
    public GameObject[,] fieldObj = new GameObject[size,size];
    System.Random rnd = new System.Random();
    // Use this for initialization
    void Start () {
        //генерирование поля без изолированных клеток
        do
        {
            GenerateField();
            CompleteMaze(ref Checkfield, 0, 0);
        } while (CheckConnections() == 0);

        int posX=0;
		int posY=0;
	for (int i=0;i<size;i++)
		{posX=0;
			for(int j=0;j<size;j++)
				{
					switch(field[i,j])
					{
						case 0:
						posX+=1;
						fieldObj[i,j]=null;
						break;
						case -1:
						//Instantiate(Plate,new Vector3(posX,0,posY),Quaternion.identity);
						posX+=1;
						fieldObj[i,j]=null;
						break;
						case 1:fieldObj[i,j]=Instantiate(SimplePlate,new Vector3(posX,0,posY),Quaternion.identity);posX+=1;break;
						case 2:fieldObj[i,j]=Instantiate(EnemyPlate,new Vector3(posX,0,posY),Quaternion.identity);posX+=1;break;
						case 3:fieldObj[i,j]=Instantiate(TreasurePlate,new Vector3(posX,0,posY),Quaternion.identity);posX+=1;break;
					}
				}
				posY+=1;
		}
		SetPlayer(0,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
