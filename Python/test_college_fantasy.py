import pytest
from college_fantasy import group_list, add_to_dict, get_weekly_score, change_list_order, read_file, get_list_sum, get_rank

def test_group_list():
    list = [1, "2", 3, 4, 5, "Hello"]
    new_list = group_list(list, 2)
    assert new_list == [[1,"2",3],[4,5,"Hello"]]
    new_list = group_list(list, 1)
    assert new_list == [1,"2",3,4,5,"Hello"]
    new_list = group_list(list, 3)
    assert new_list == [[1,"2"],[3,4],[5,"Hello"]]
    new_list = group_list(list, 5)
    assert new_list == [[1],["2"],[3],[4],[5,"Hello"]]
    new_list = group_list(list, 6)
    assert new_list == [[1],["2"],[3],[4],[5],["Hello"]]
    
def test_add_to_dict():
    dict = {}
    add_to_dict(dict, "Hello", 1)
    assert dict["Hello"] == [1]
    dict = {"1": 1}
    add_to_dict(dict, "2", "2")
    assert dict["2"] == ["2"]
    dict = {1:1, 2:2, 3:3}
    add_to_dict(dict, 4, 4)
    assert dict[4] == [4]
    dict = {"1":"1", "2":"2", "3":"3"}
    add_to_dict(dict, "4", "4")
    assert dict["4"] == ["4"]
    dict = {"Hello": "World", 1:1, "2":2, 3:"3"}
    add_to_dict(dict, 4, "4")
    assert dict[4] == ["4"]
    dict = {"Hello":"World", 1:1, 2:2}
    add_to_dict(dict, 1, 4)
    assert dict[1] == [1, 4]
    dict = {"Hello":["World", 3, "test", 7], 1:1, 2:2}
    add_to_dict(dict, "Hello", "great")
    assert dict["Hello"] == ["World", 3, "test", 7, "great"]

def test_get_weekly_score():
    assert get_weekly_score('y','y', 22) == 72
    assert get_weekly_score('y','n', 0) == 40
    assert get_weekly_score('y','y', 0) == 50
    assert get_weekly_score('n','n', 1) == 1
    assert get_weekly_score('n','n', 0) == 0
    assert get_weekly_score('y','y', 25) == 75
    assert get_weekly_score('y','y', 5) == 55
    assert get_weekly_score('y','n', 9) == 49
    assert get_weekly_score('n','n', 8) == 8
    assert get_weekly_score('y','n', 16) == 56

def test_change_list_order():
    dict = {"1": [1, 2]}
    change_list_order(dict, "1", 1)
    assert dict["1"] == [2, 1]
    dict = {1:[1, 2], 2:[2, 3], 3:[3, 4]}
    change_list_order(dict, 2, 2)
    assert dict[2] == [3, 2]
    dict = {"1":["1", "2", "3"], "2":["2"], "3":["3"]}
    change_list_order(dict, "1", "2")
    assert dict["1"] == ["1", "3", "2"]
    dict = {"Hello": ["World", 1, 3, 5], "2":[2], 3:["3"]}
    change_list_order(dict, "Hello", "World")
    assert dict["Hello"] == [1, 3, 5, "World"]
    dict = {"Hello":["World", 3, "test", 7], 1:[1], 2:[2]}
    change_list_order(dict, "Hello", 3)
    assert dict["Hello"] == ["World","test", 7, 3]

def test_read_file():
    filename = "test_read_file.txt"
    dict = read_file(filename)
    assert dict["Hello"] == ["World"]
    assert dict["1"] == ["2", "3", "4", "5"]
    assert dict["Madison"] == ["Loutensock"]
    assert dict["Python"] == ["is", "fun"]
    assert dict["2"] == ["the", "second", "number"]
    assert dict["first"] == ["is", "the", "number", "1"]
    
def test_get_list_sum():
    list = [1,2,3,4,5,6,7,8,9,10]
    assert get_list_sum(list) == 55
    list = ['1','2','3','4','5','6','7','8','9','10']
    assert get_list_sum(list) == 55
    list = [100, 354, 2, 67, 95]
    assert get_list_sum(list) == 618
    list = ['100', '354', '2', '67', '95']
    assert get_list_sum(list) == 618
    list = [4,6,'10','12',44]
    assert get_list_sum(list) == 76
    list = [2]
    assert get_list_sum(list) == 2

def test_get_rank():
    dict = {}
    assert get_rank(dict, "Hello") == 0
    dict = {"1": 1, 1:16, 2:[2], 3:[3], "2":"2", "3":["10"], "Hello":7, 4:[24]}
    assert get_rank(dict, "test") == 0
    assert get_rank(dict, "4") == 0
    assert get_rank(dict, "2") == 24
    assert get_rank(dict, "3") == 16
    assert get_rank(dict, 1) == 10
    assert get_rank(dict, "Hello") == 19
    assert get_rank(dict, 4) == 2

pytest.main(["-v", "--tb=line", "-rN", __file__])
