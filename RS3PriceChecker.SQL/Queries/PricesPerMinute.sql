 Declare @startMinute smalldatetime Set @startMinute = '2021/12/20 18:00:00'
 Declare @endMinute smalldatetime Set @endMinute = '2021/12/21 17:59:59';
 With minuteList(aMinute) As 
 (Select @startMinute Union All
    Select dateadd(minute,1, aMinute)
    From minuteList
    Where aMinute < @endMinute)
 Select aMinute, [Count] = Count(T.[ModifiedDate])
 From minuteList ml Left Join [RS3PriceChecker].[dbo].[Price] T
      On DateAdd(minute, DateDiff(minute, 0, T.[ModifiedDate]), 0) = aMinute
 Group By aMinute
 HAVING Count(T.[ModifiedDate]) > 1

 Option (MaxRecursion 10000)
