using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPattern :MonoBehaviour
{
    public List<int[,]> patternList = new List<int[,]>();

    private void Start()
    {
        SetPattern();
    }

    public int[,] GetPattern(int roomNumber)
    {
        if (roomNumber == 1) // 시작방 / 보스방
        {
            return patternList[0];
        }
        if (roomNumber == 3)
        {
            return patternList[1];
        }
        if (roomNumber == 4) // 상점방
        {
            return patternList[2]; 
        }
        if (roomNumber == 5) // 황금방
        {
            return patternList[3];
        }
        if (roomNumber == 6) // 저주방
        {
            return patternList[4];
        }

        // 위의 방에 전부 포함되어있지않으면 "일반방"
        int rand = Random.Range(5, patternList.Count);
        return patternList[rand]; // 랜덤한 패턴을 리턴.
    }

    void SetPattern()
    {
        int[,] tmp;
        // 0 : 빈칸
        // 1 : 돌
        // 2 : 똥
        // 3 : 모닥불
        // 4 : 가시

        // 5 : 몬스터

        // 6 : 아이템 테이블 ( 상점용 )
        // 7 : 아이템 테이블 ( 황금방 )
        // 8 : 일반 상자 ( 열쇠 X)
        // 9 : 황금 상자 ( 열쇠 O ) 

        // 10 : 플레이어 생성 위치

        /* 시작방 */
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 6, 7, 7, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 2, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 3, 0, 4, 0, 0, 0, 0, 9, 8, 9, 9, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        /* 보스방 */
        tmp = new int[7, 13] {
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        /* 상점방 */
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 6, 0, 6, 0, 6, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 6, 0, 6, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        /* 황금방 */
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        /* 저주방 */
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 8, 9, 8, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);


        /* 일반방 패턴 */
        // 패턴 1번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 2번
        tmp = new int[7, 13]{
            {2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 2 },
            {2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 2 },
            {2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 2 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 2 },
            {2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 2 },
            {2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 2 }};
        patternList.Add(tmp);

        // 패턴 3번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 1, 8, 1, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 4번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 5번
        tmp = new int[7, 13] {
            {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 }};
        patternList.Add(tmp);

        // 패턴 6번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {0, 0, 1, 5, 1, 0, 1, 0, 1, 5, 1, 0, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 7번
        tmp = new int[7, 13] {
            {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {1, 8, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 8, 1 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 }};
        patternList.Add(tmp);

        // 패턴 8번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 5, 1, 4, 1, 5, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 4, 4, 4, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 5, 1, 4, 1, 5, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 9번
        tmp = new int[7, 13] {
            {3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 }};
        patternList.Add(tmp);

        // 패턴 10번
        tmp = new int[7, 13] {
            {5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            {0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 2, 0, 5, 0, 2, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
            {5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }};
        patternList.Add(tmp);

        // 패턴 11번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 5, 0, 5, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 5, 0, 5, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 12번
        tmp = new int[7, 13] {
            {3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 5, 0, 0, 3, 0, 0, 5, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 }};
        patternList.Add(tmp);

        // 패턴 13번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            {8, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {1, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 5, 3, 5, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 1 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 8 },
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 14번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 9 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
            {0, 0, 0, 0, 0, 5, 4, 5, 0, 0, 0, 1, 1 },
            {0, 0, 0, 0, 0, 4, 3, 4, 0, 0, 0, 0, 0 },
            {1, 1, 0, 0, 0, 5, 4, 5, 0, 0, 0, 0, 0 },
            {1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {9, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 15번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
            {0, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 0, 0 },
            {0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 16번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 4, 0, 0, 0, 4, 4, 4, 0, 0 },
            {4, 4, 4, 0, 4, 0, 0, 0, 4, 8, 4, 0, 0 },
            {0, 0, 4, 0, 4, 0, 0, 0, 4, 0, 4, 0, 0 },
            {0, 0, 4, 8, 4, 0, 0, 0, 4, 0, 4, 4, 4 },
            {0, 0, 4, 4, 4, 0, 0, 0, 4, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 17번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 5, 0 },
            {0, 0, 0, 0, 0, 5, 1, 0, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 1, 8, 1, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 0, 1, 5, 0, 0, 0, 0, 0 },
            {0, 5, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);
        
        // 패턴 18번
        tmp = new int[7, 13] {
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 8 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {8, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }};
        patternList.Add(tmp);

        // 패턴 19번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 4, 0, 4, 0, 4, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 4, 4, 4, 0, 0, 0, 0, 0 },
            {0, 0, 5, 0, 4, 4, 4, 4, 4, 0, 5, 0, 0 },
            {0, 0, 0, 0, 0, 4, 4, 4, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 4, 0, 4, 0, 4, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 20번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            {0, 0, 0, 0, 0, 3, 5, 3, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 5, 3, 5, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 3, 5, 3, 0, 0, 0, 0, 0 },
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 21번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 1, 1, 5, 1, 1, 1, 0, 0, 0 },
            {0, 0, 5, 1, 8, 1, 0, 1, 8, 1, 5, 0, 0 },
            {0, 0, 0, 1, 1, 1, 5, 1, 1, 1, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 22번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            {0, 1, 0, 0, 0, 5, 3, 5, 0, 0, 0, 1, 0 },
            {0, 0, 0, 0, 0, 3, 9, 3, 0, 0, 0, 0, 0 },
            {0, 1, 0, 0, 0, 5, 3, 5, 0, 0, 0, 1, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 23번
        tmp = new int[7, 13] {
            {1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            {1, 8, 1, 5, 0, 0, 0, 0, 0, 5, 0, 0, 0 },
            {1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
            {0, 0, 0, 5, 0, 0, 0, 0, 0, 5, 1, 8, 1 },
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 }};
        patternList.Add(tmp);

        // 패턴 24번
        tmp = new int[7, 13] {
            {5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            {0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0 },
            {0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0 },
            {5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 }};
        patternList.Add(tmp);

        // 패턴 25번
        tmp = new int[7, 13] {
            {3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 5, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 },
            {1, 0, 0, 0, 0, 0, 1, 0, 5, 0, 1, 0, 0 },
            {0, 1, 0, 0, 0, 3, 0, 3, 0, 0, 0, 1, 0 },
            {0, 0, 1, 0, 5, 0, 1, 0, 0, 0, 0, 0, 1 },
            {0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 5, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 3 }};
        patternList.Add(tmp);

        // 패턴 26번
        tmp = new int[7, 13] {
            {2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2 },
            {2, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 0, 2 },
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 2, 1, 2, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
            {2, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 0, 2 },
            {2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2 }};
        patternList.Add(tmp);

        // 패턴 27번
        tmp = new int[7, 13] {
            {0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
            {0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 2, 5, 2, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 5, 2, 5, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 2, 5, 2, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0 },
            {0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 28번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 2, 5, 0, 0, 0, 0, 0, 5, 1, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 1, 5, 0, 0, 0, 0, 0, 5, 2, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 29번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 5, 1, 5, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 2, 8, 2, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 5, 1, 5, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 28번
        tmp = new int[7, 13] {
            {3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 },
            {0, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3 }};
        patternList.Add(tmp);

        // 패턴 30번
        tmp = new int[7, 13] {
            {0, 0, 5, 2, 0, 0, 0, 0, 0, 2, 5, 0, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            {0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0 },
            {0, 0, 0, 0, 0, 3, 8, 3, 0, 0, 0, 0, 0 },
            {0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            {0, 0, 5, 2, 0, 0, 0, 0, 0, 2, 5, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 31번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
            {0, 0, 0, 0, 0, 0, 5, 0, 1, 0, 0, 0, 0 },
            {0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0 },
            {0, 0, 0, 0, 1, 0, 5, 0, 0, 0, 0, 0, 0 },
            {0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 32번
        tmp = new int[7, 13] {
            {1, 3, 1, 3, 1, 0, 0, 0, 1, 3, 1, 3, 1 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
            {0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0 },
            {0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {1, 3, 1, 3, 1, 0, 0, 0, 1, 3, 1, 3, 1 }};
        patternList.Add(tmp);

        // 패턴 33번
        tmp = new int[7, 13] {
            {0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 },
            {0, 1, 0, 3, 0, 1, 0, 3, 0, 1, 0, 3, 0 },
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
            {0, 3, 0, 1, 0, 3, 0, 1, 0, 3, 0, 1, 0 },
            {0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 }};
        patternList.Add(tmp);

        // 패턴 34번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            {0, 0, 1, 0, 9, 0, 8, 0, 9, 0, 1, 0, 0 },
            {0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            {0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 35번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 5, 0, 1, 0, 5, 0, 0, 0, 0 },
            {0, 0, 0, 0, 3, 0, 5, 0, 3, 0, 0, 0, 0 },
            {0, 0, 0, 0, 5, 0, 1, 0, 5, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 36번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 4, 0, 0, 0, 0, 0, 4, 0, 0, 0 },
            {0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0 },
            {0, 0, 0, 0, 5, 0, 0, 0, 5, 0, 0, 0, 0 },
            {0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0 },
            {0, 0, 0, 4, 0, 0, 0, 0, 0, 4, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 37번
        tmp = new int[7, 13] {
            {2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2 },
            {2, 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0 },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            {2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 5, 3, 2 },
            {2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2 }};
        patternList.Add(tmp);

        // 패턴 38번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 1, 1, 0, 5, 0, 5, 0, 1, 1, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);

        // 패턴 39번
        tmp = new int[7, 13] {
            {1, 0, 4, 0, 0, 0, 0, 0, 0, 2, 2, 1, 8 },
            {0, 0, 0, 4, 0, 0, 0, 0, 5, 5, 2, 1, 1 },
            {0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 4, 0, 0, 0, 0, 5, 5, 2, 1, 1 },
            {1, 0, 4, 0, 0, 0, 0, 0, 0, 2, 2, 1, 8 }};
        patternList.Add(tmp);

        // 패턴 40번
        tmp = new int[7, 13] {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        patternList.Add(tmp);
    }
}

// 0 : 빈칸
// 1 : 돌
// 2 : 똥
// 3 : 모닥불
// 4 : 가시
// 5 : 몬스터
// 8 : 일반 상자 ( 열쇠 X)
// 9 : 황금 상자 ( 열쇠 O ) 

