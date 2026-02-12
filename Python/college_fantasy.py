"""
College Fantasy Football Program
Purpose: To help with the draft and scoring of a college fantasy football league
Programmer: Madison Loutensock

How to Play/Test:
For some information on how my program works, first you will need to input 0 for the week to build a list 
of players and their teams. Each player will choose 4 college football teams. This will create a file called player_teams. 

Then if you type a number between 1-14, it will check the cff_teams_schedule file for the weeks each team plays. 
Because of this, when you build the teams in week 0, you'll need to pick teams that are listed in 
the cff_teams_schedule file. That file is based on the teams my family used when we played in 2024.

Each team playing in the specified week will get a score based on whether they won their game that week and
got the betting spread. If they won, that team will get 40 points and if they got the spread, they will get 10 points.
The ap_poll file gives the top 25 ranked teams from the ap poll in 2024, for week 4. This will be used to 
get a rank score for each team. Each ranked team gets 26 - their rank, in points. Normally this list would change 
every week but for the sake of testing the program, I've just been using the same list.

Each player gets three team scores every week, so they will only get points from their fourth team if another
team is on a bye week. If multiple teams have byes, they will get a previous week's score from their bye team
until they have three scores.

Week 1 will create three files: one for each team's weekly scores called team_scores, one for each player's
weekly scores called player_scores, and another for the bye score weeks each player has used. These files
will then be used in weeks 2-14, to calculate running scores.

For the "bye" option in the first input, each player has the opportunity to switch which team is their bye team
one time during the season.
"""
import random
import csv

def group_list(list, num_groups):
    
    """Convert a list into a compound list with a specified number of groups

    Parameters:
        list: a list to be converted into a compound list
        num_groups: number of groups to separate a list into 
    Return: 
        a compound list with the specified number of groups 
        OR the list as before, if num_groups == 1
    """
    
    if num_groups != 1:
        try:
            # if number of groups is greater than the length of the list, raise Value Error
            if num_groups > len(list):
                raise ValueError("Number of groups must be less than length of list")
            
            #find the average so each group has approximately the same amount
            avg = len(list) / num_groups
            #empty list for groups
            new_list = []
            count = 0
            while count < len(list):
                #append items into compound list starting with the count number and going up to the average
                new_list.append(list[int(count):int(count + avg)])
                #add average to count so the next iteration of the loop will do the next group in original list
                count += avg
            #return compound list     
            return new_list
        
        except ValueError as val_err:
            print("Error:", val_err)
    else:
        #if num_groups == 1, return original list
        return list

def add_to_dict(dict, key, value):
    """Use a key and a value to add to an existing dictionary
    Parameters:
        dict: a dictionary to be added to
        key: the key to be added to the dictionary
        value: the value to be added alongside the key 
    Return: 
        the dictionary with new key and value added"""
    if key in dict.keys():
        if not isinstance(dict[key], list):
            dict[key] = [dict[key]] 
        # append value to key list if key already exists in dictionary
        dict[key].append(value)
    else:
        # add both key and value to dictionary if key is not in it already
        dict[key] = [value]
    return dict

def get_weekly_score(win, spread, rank_score):
    """Use win, spread, and rank_score to get a weekly score for each team
    Parameters:
        win: a string ("y" or "n") representing whether a team won their game
        spread: a string ("y" or "n") representing whether a team got the spread for their game
        rank_score: a number representing the points a team received for their ranking in the ap poll 
    Return: 
        score: total weekly score for the team"""
    score = 0
    # if the team won, add 40 points
    if win.lower() == "y":
        score += 40
    # if the team got the spread, add 10 points
    if spread.lower() == "y":
        score += 10
    # add the team's rank score to score
    score += rank_score
    return score

def change_list_order(dict, key, new_last_item):
    """Switch the order of two items in a list from a dictionary
    Parameters:
        dict: a dictionary to be changed
        key: key from dictionary that will have its list changed
        new_last_item: the value that will be moved to the end of the list
    Return: 
        dict: dictionary with new_last_item moved to the end of the key's list"""
    # find new_last_item's original index in list
    original_index = dict[key].index(new_last_item)
    # remove item from list and put into removed_item variable
    removed_item = dict[key].pop(original_index)
    # add removed_item back into list, at the end
    dict[key].append(removed_item)

    return dict

def read_file(filename):
    """read a file and return information in a dictionary
    Parameters:
        filename: a file to be read and added to a dictionary 
    Return: 
        text_dict: a new dictionary created from information in the file"""
    # create empty dictionary
    text_dict = {}

    # Open the text file for reading
    with open(filename, "rt") as f:
        
        reader = csv.reader(f, strict=True)

        # Read the contents of the text file one line at a time.
        for row in reader:
            key = row[0]
            # put values in list format
            values = row[1:]
            # create empty list
            clean_values = []
            # strip white space and add each item to clean_values list
            for item in values:
                clean_item = item.strip()
                clean_values.append(clean_item)
            # add clean values to the dictionary key list
            text_dict[key] = clean_values

    # Return the dictionary that contains the lines of text.
    return text_dict

def get_list_sum(list):
    """add up all the numbers in a list to get a sum
    Parameters:
        list: a list of numbers to be added together 
    Return: 
        total: a total of all numbers from list added together"""
    total = 0
    # iterate through list
    for num in list:
        # add score to total score
        total += int(num)
    return total

def get_rank(dict, key):
    """determine each team's score based on their rank in the AP poll
    Parameters:
        dict: a dictionary with the top 25 teams from the AP poll
        key: key to be checked 
    Return: 
        rank: rank score for the specified key"""
    if key in dict.keys():
        # turn key value into a list
        if not isinstance(dict[key], list):
            dict[key] = [dict[key]]
        # turn value into an integer
        value = int(dict[key][0])
        # if key in dict, subtract value from 26 to get rank score
        rank = 26 - value
    else:
        # if key not in dict, rank score = 0
        rank = 0
    return rank

def write_file(filename, dict, num):
    """write a file to store information
    Parameters:
        filename: a file to be written
        dict: dictionary with information to write to file
        num: number to specify what kind of dictionary dict is: 1 meaning there is only one value per key
    Return: 
        nothing"""
    # open filename to write mode
    with open(filename, "w") as f:
        if num == "1":
            # if num == 1, write each key and value from dict to file
            for key, value in dict.items():
                # turn each value into a string
                str_value = str(value)
                # write key and value to file
                print(f"{key}, {str_value}", file=f)
        else:
            # add key values from a list
            for key, value_list in dict.items():
                string_values = []
                for value in value_list:
                    # turn each value into a string
                    str_value = str(value)
                    string_values.append(str_value)
                # Join list elements with a comma and space
                formatted_value = ", ".join((string_values))
                # Write key and value separated by commas
                print(f"{key}, {formatted_value}", file=f)


def main():
    try:
        week = input('What week number is it? (Type 0 to start a new league or "bye" to change a bye team): ')
        if week == "0":
            #Set-up for new games:
            print()
            num_player = int(input("How many people are in the league? "))

            # if num_player input is 0, raise ValueError
            if num_player == 0:
                raise ValueError("Number of players must be at least 1")
            
            # create empty list of names and start count
            names = []
            count = 0
            while count < num_player:
                # get the names of each player until count == number of players
                count += 1
                name = input("Player name: ")
                # add name to names list
                names.append(name)

            # get number of groups for draft
            print()
            group_num = int(input("How many groups do you want to have for the draft? "))
            
            # if group_num == 0, raise the Zero Division Error
            if group_num == 0:
                raise ZeroDivisionError("Number of groups must be at least 1")
            
            # randomly shuffle names and add to new shuffled list
            random.shuffle(names)
            #call function group_list to turn list into a compound list with the desired number of groups
            grouped_names = group_list(names, group_num)
            
            #loop through and print each group, with the draft pick order for each name
            teams_dict = {}
            if group_num != 1:
                for i, group in enumerate(grouped_names):
                    print()
                    print(f"-- Group: {i+1} --")
                    for i, name in enumerate(group):
                        print(f"{i+1}. {name}")

                #loop through each person, by group, in alternating order
                for i, group in enumerate(grouped_names):
                    pick = 0
                    count = 0
                    print()
                    print(f"-- Group {i+1} Picks --")
                    #loop through each name in the groups until each person has 4 teams
                    while pick < 4 * len(group):
                        if count % 2 == 0:
                            for name in group:
                                team = input(f"Pick {pick + 1} - {name}: ")
                                capital_team = team.title()
                                #call create_dict function to add name and team to the teams_dict dictionary
                                add_to_dict(teams_dict, name, capital_team)
                                pick += 1
                        else:
                            for name in reversed(group):
                                team = input(f"Pick {pick + 1} - {name}: ")
                                capital_team = team.title()
                                #call create_dict function to add name and team to the teams_dict dictionary
                                add_to_dict(teams_dict, name, capital_team)
                                pick += 1
                        count += 1
            else: 
                # loop through grouped names with the draft order
                print()
                print(f"-- Draft Order --")
                for i, name in enumerate(grouped_names):
                    print(f"{i+1}. {name}")
                
                pick = 0
                count = 0  
                #loop through each name until each person has 4 teams
                while pick < 4 * len(grouped_names):
                    if count % 2 == 0:
                        for name in grouped_names:
                            team = input(f"Pick {pick + 1} - {name}: ")
                            capital_team = team.title()
                            #call create_dict function to add name and team to the teams_dict dictionary
                            add_to_dict(teams_dict, name, capital_team)
                            pick += 1
                    else:
                        for name in reversed(grouped_names):
                            team = input(f"Pick {pick + 1} - {name}: ")
                            capital_team = team.title()
                            #call create_dict function to add name and team to the teams_dict dictionary
                            add_to_dict(teams_dict, name, capital_team)
                            pick += 1
                    count += 1
            # write all players and their chosen teams to a new file
            player_teams_file = "player_teams.txt"
            write_file(player_teams_file, teams_dict, week)
        
        elif week.lower() == "bye":
            # call read_file function to read the user's player_teams file into a dictionary
            player_teams_file = "player_teams.txt"
            player_teams_dict = read_file(player_teams_file)

            bye_week_file = "bye_weeks_used.txt"
            bye_week_dict = read_file(bye_week_file)

            # iterate through each player in the player_teams_dict
            print()
            print("Players:")
            for player in player_teams_dict.keys():
                # print each player
                print(player)

            # get input from the user on which player's teams to change
            print()
            bye_change_player = input("Which player is changing their bye team? ")

            if bye_change_player not in player_teams_dict.keys():
                raise KeyError("You must pick a player from the list")
            
            for player, bye_week_list in bye_week_dict.items():
                if player == bye_change_player:
                    if any("changed" in item for item in bye_week_list):
                        raise KeyError("This player has already changed their bye team this season")
                    else:
                        pass
            
            # use the bye_change_player variable to iterate through that player's teams
            print()
            print(f"{bye_change_player}'s Teams:")
            for team in player_teams_dict[bye_change_player]:
                # print each team for the player
                print(team)
            
            # get input for which team to switch out for the last team in their list
            print()
            new_bye = input(f"Which team would you like to switch out for {player_teams_dict[bye_change_player][-1]}? ")
            capital_new_bye = new_bye.title()

            if capital_new_bye not in player_teams_dict[bye_change_player]:
                raise Exception("You must pick a team from the list")
            
            # call change_list_order function to make the team from the new_bye variable the last one in the player's list
            change_list_order(player_teams_dict, bye_change_player, capital_new_bye)

            # call write_file function to rewrite the player_teams_file using the changed list
            write_file(player_teams_file, player_teams_dict, week)

            # ask user for upcoming week
            upcoming_week = input("What is the next upcoming week in the season? ")
            # add "changed before week #" to bye_weeks_used file
            add_to_dict(bye_week_dict, bye_change_player, f"changed before week {upcoming_week}")
            write_file(bye_week_file, bye_week_dict, week)
            
            # print the player's name and their new bye team
            print(f"{bye_change_player}'s bye team was changed to {player_teams_dict[bye_change_player][-1]}")

        elif week == "1" or week == "2" or week == "3" or week == "4" or week == "5" or week == "6" or week == "7" or week == "8" or week == "9" or week == "10" or week == "11" or week == "12" or week == "13" or week == "14":
            # create empty list and dictionary
            playing_teams_list = []
            all_teams_list = []
            all_teams_dict = {}
            # call read_file function to populate dictionaries for ap poll ranks and the team schedule
            ap_poll_dict = read_file("ap_poll.txt")
            schedule_dict = read_file("cff_teams_schedule.txt")

            # iterate through each school in the schedule dictionary
            for school, games in schedule_dict.items():
                # add the school to playing_teams_list if they are playing in the specified week
                # add the school to list of all teams
                if week in games:
                    playing_teams_list.append(school)
                    all_teams_list.append(school)
                # add school to list of all teams
                else:
                    all_teams_list.append(school)
            
            # print all teams playing in the specified week
            print(f"Teams playing in Week {week}:")
            for playing_team in playing_teams_list:
                print(playing_team)

            # iterate through each team in all_teams_list
            for team in all_teams_list:
                team_score = 0
                if team in playing_teams_list:
                    # have user input whether the team won and got the spread for the specified week
                    print()
                    win = input(f"Did {team} win their game this week (y/n)? ")
                    if win.lower() != "y" and win.lower() != "n":
                        raise TypeError("You must type either 'y' or 'n'")
                    spread = input(f"Did {team} get the spread for this game (y/n)? ")
                    if spread.lower() != "y" and spread.lower() != "n":
                        raise TypeError("You must type either 'y' or 'n'") 
                    # call the get_rank function to get the rank score for the team based on the ap_poll_dict
                    rank = get_rank(ap_poll_dict, team)

                    # call get_weekly_score function to compute the team's score for the week 
                    # based on the win, spread, and rank variables
                    team_score = get_weekly_score(win, spread, rank)

                    # add the team and score to the all_teams_dict
                    all_teams_dict[team] = team_score
                    # print the team's score for the week
                    print(f"{team} scored {team_score} points for week {week}")
                else:
                    # if not playing, add the team to all_teams_dict with value as 'bye'
                    all_teams_dict[team] = "bye"

            team_scores_file = "team_scores.txt"
            if week == "1":
                #if it is week 1, call write_file function to create a new file with the team scores for week 1
                write_file(team_scores_file, all_teams_dict, week)
            
            # for all subsuquent weeks
            else:
                # call read_file function to create a dict with all previous team scores
                team_scores_dict = read_file(team_scores_file)
                # iterate through all_teams_dict
                for team, score in all_teams_dict.items():
                    # call create_dict function to add new scores to team_scores_dict
                    add_to_dict(team_scores_dict, team, score)
                # call write_file function to rewrite team_scores_file with the new scores added
                write_file(team_scores_file, team_scores_dict, week)


            # call read_file function to populate teams_dict, using the previously created player_teams file
            teams_dict = read_file("player_teams.txt")
            # empty weekly_score_dict
            weekly_score_dict = {}
            
            #read bye_weeks_used file if it is not week 1, else create an empty dictionary
            if week != "1":
                bye_week_dict = read_file("bye_weeks_used.txt")
            else:
                bye_week_dict = {}

            # iterate through each player in teams_dict
            for player, chosen_teams in teams_dict.items():
                weekly_score = 0
                score_count = 0
                bye_count = 0
                # loop through until each player has three scores for the week
                while score_count < 3:
                    # iterate through each player's teams
                    for chosen_team in chosen_teams:
                        # if team is playing in specified week, add their score from playing_teams_dict to the
                        # player's weekly_score
                        if chosen_team in playing_teams_list and score_count < 3:
                            team_score = int(all_teams_dict[chosen_team])
                            weekly_score += team_score
                            # add 1 to score count for while loop
                            score_count += 1

                            # if bye team is used, add the week to bye_week_dict
                            if chosen_team == chosen_teams[-1]:
                                bye_week = week
                                add_to_dict(bye_week_dict, player, bye_week)
                        else:
                            # if the team is not playing in specified week, add one to the bye count
                            bye_count += 1
                    # if player has more than one bye, loop through until their score count is three
                    while bye_count > 1 and score_count < 3:
                        # use their fourth team to get scores when one or more of their teams is not playing
                        team_score = int(input(f"Please enter the most recent unused score for {chosen_teams[-1]} for {player}: "))
                        # add inputted score to weekly score

                        # ask user for the week the bye score is from
                        bye_week = input("Please enter the week this score is from: ")
                        # add bye week to bye_week_dict
                        add_to_dict(bye_week_dict, player, bye_week)

                        weekly_score += team_score
                        score_count += 1
                # add the player's total score to weekly_score_dict as a string
                weekly_score_dict[player] = weekly_score
                # print player and their score for specified week
            
            # write bye_weeks_used.txt file using bye_week_dict
            bye_week_file = "bye_weeks_used.txt"
            write_file(bye_week_file, bye_week_dict, week)

            # create lambda function to extract the score from a list
            extract_score = lambda extracted_score: extracted_score[1]

            # call lambda function extract_score to sort scores_dict by score in descending order
            sorted_scores_dict = dict(sorted(weekly_score_dict.items(), key=extract_score, reverse=True))

            print()
            print(f"Week {week} Scores:")
            for i, (player, week_score) in enumerate(sorted_scores_dict.items()):
                print(f"{i+1}. {player} - {week_score}")

            player_scores_file = "player_scores.txt"
            if week == "1":
                # if week 1, call write_file function to create a new file with every player's score for week 1
                write_file(player_scores_file, weekly_score_dict, week)
                
            else:
                # call read_file function to create a dict with all previous weekly scores
                total_scores_dict = read_file(player_scores_file)
                # iterate through weekly_score_dict
                for player, score in weekly_score_dict.items():
                    # call create_dict function to add scores to total_scores_dict
                    add_to_dict(total_scores_dict, player, score)
                # call write_file function to rewrite player_scores_file with newly added scores
                write_file(player_scores_file, total_scores_dict, week)

                # call get_running_score function to total each player's scores from all previous weeks
                running_scores_dict = {}
                # iterate through total_scores_dict
                for player in total_scores_dict.keys():
                    # call get_running_score function to total all scores for each player
                    total_score = get_list_sum(total_scores_dict[player])
                    # add total score to running_scores_dict for each player
                    running_scores_dict[player] = total_score

                # call lambda function extract score to sort the running_scores_dict by score in descending order
                sorted_running_scores_dict = dict(sorted(running_scores_dict.items(), key=extract_score, reverse=True))
                # print 
                print()
                print("Running Scores: ")
                # iterate through sorted_running_scores_dict
                for i, (player, running_score) in enumerate(sorted_running_scores_dict.items()):
                    # print each player and their running score with their rank
                    print(f"{i+1}. {player} - {running_score}")
        else:
            print(f"Error: Week must be a number between 0-14 or 'bye'\nPlease try again")
            raise ValueError()
    except ValueError as val_err:
        print("Error: Please enter a whole number")
        print(val_err)
    except TypeError as type_err:
        print("Error:", type_err)
    except KeyError as key_err:
        print ("Error:", key_err)
    except FileNotFoundError as file_err:
        print("Error:", file_err)
    except ZeroDivisionError as zerodiv_err:
        print("Error: Cannot divide by zero")
        print(zerodiv_err)
    except Exception as excep:
        print("Error: Something went wrong, please try again")
        print(excep)

            
if __name__ == "__main__":
    main()
