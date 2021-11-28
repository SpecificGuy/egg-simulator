# Metamon Island Simulator
Tool designed for an extensive and deep growing analysis for an account playing Metamon Island NFT Game.

## Before using Metamon Island Simulator ##

This software is completely free to use and open source.
It takes A LOT of effort to develop, maintain and keep it updated.

Please, if you find it any useful consider donating some kudos to the address in the program. I appreciate you so much! :heartpulse:
# Installation #

To download the application go to Code above in this page and the click on Download ZIP.
You can anyway pull or clone the repository with git as any other repository out there.


![Download ZIP](https://github.com/SpecificGuy/egg-simulator/blob/main/docs/Download.png?raw=true)*Download the zip file*

Then on the folder downloaded there's a folder Release where the compiled program resides.

You can start the simulator with EggCalculator.exe executable file.
# User manual #
## Calendar ##
---
![Calendar view](https://github.com/SpecificGuy/egg-simulator/blob/main/docs/Calendar.png?raw=true)*Calendar view as the application is started.*

The calendar view shows account growing day by day.
The simulation starts from today, using the starting account data inserted by the user on the [Configuration](#configuration) panel.

Actually there is not way to make the simulation start from a configurable date.

Day by day matches are simulated accordingly to the fragments rates, and fragments are added to the daily balance.

These fragments are then minted by the simulation into eggs and sold to the market the the price configured on the [Configuration](#configuration) panel.

On the Calendar the main events are highlighted such as Eggs quantity, and Metamon purchases.

By clicking on any day you can see the right section of the panel changing accordingly to the simulated day.

User can also change the month visualized by clicking on the arrows on the top of the panel.

### Simulation data ###
---
The right section of the program lists all the useful data of the day.
In particular, the above section shows:

1.  Date - Date of the simulation selected on the calendar
2.  Raca Balance - Raca balance at the end of the day. This means that the balance is comprehensive of the simulation of the day selected
3. Fragment Balance - Total account fragments at the end of the simulated day
4. Egg Balance - Total account eggs remaining at the end of the simulated day
5. Total Metamon - Total metamon count at the end of the simulated day
6. Level Ups - Total Level Up counts during the day 
7. League Ups [Deprecated] - Total League promotions during the day
8. Daily Fragments - Fragments farmed during the day
9. Matches played - Total matches played by all metamons during the day
10. Matches cost - Total matches cost spent for playing the game
11. Eggs Minted - Eggs minted during the day
12. Gross Daily Cost - Total RACA spent for potions and matches during the day
13. Gross Daily Gain - Total RACA gained by egg selling.

Additionally, the Potions and Diamonds count are listed. The count of all these level up items are at the end of the simulation day.

The user can also manually execute the following operations:
1.  Mint Eggs - If the Auto minting on the [Configuration](#configuration) is disabled, the user can Mint eggs using the fragments farmed on the day simulated
2.  Sell Eggs - If the Auto selling on the [Configuration](#configuration) is disabled, the user can sells eggs on the market on the price configured. The RACA Balance is increased by this operation
3. Add RACA - The operator can decide to add additional RACA to the account to increase the simulation resouces.
4. Buy Potion, Yellow Diamond, Purple Diamond, Black Diamond - Uses the RACA Balance to buy level up items at the price configured on [Configuration](#configuration)

Below are also listed the account metamons on the day selected. By selecting a metamon the user can

1. Add a Metamon - User can add a Metamon by clicking on the corresponding Rarity button. The RACA Balance must have at its disposal the correct amount to buy it. The price is configured on the [Configuration](#configuration) panel.
2. Remove the metamon from the list. Actually the metamon is simply remove and not sold, so the RACA balance remain unaffected by this operator
3. Level Up metamon - User can level up the metamon selected in the list. The corresponding level up item must be available in the account and the experience must be the correct one for the operator to take place.

Below the metamon list there is a simple operation recap for the day.

### Resimulation ###
One of the most important button of the application.
If you manually change anything during a day the user must click this button in order to recalculate all the days to come, starting from the selected one.

All the previous days are unaffected by it.

**All the manual modification applied previously on some date after the one resimulated are lost, so the best-practice for the application is to apply manual changes in rigorous chronological order**
## Charts ##
---
![Chart view](https://github.com/SpecificGuy/egg-simulator/blob/main/docs/Charts.png?raw=true)*Charts tracked by the simulation provide a overview summary of all the days simulated*

Charts panel is available by clicking on the label on the top of the screen. This panel has the main purpose of showing the growing picture of the account using the data provided by the user.

On the X axis are displayed all the days simulated, counted from the first day simulated up to the last one.

The following charts are currently supported, but keep in mind that suggestions are appreciated:

1. ### Daily Gross Gain ###
Shows the curve related to the Daily Gross Gain. Actually the gain totally depends on the Eggs sold by the user, but in the future also metamon selling can be supported

2. ### Eggs ###
Eggs minting curve. It shows how much the account grew during the simulation

3. ### Fragments ###
Fragments gaining curve.

4. ### Metamons ###
Metamon count curve. Shows how much metamons are owned in each day of the simulation

5. ### Revenue ###
Revenue curve. If the Auto Revenue is enabled, the simulation can sell RACA periodically, based on the configuration provided. This chart shows each day how many RACAs are sold to provide to the owner of the account a real revenue.

6. ### Match Cost ###
Shows the match cost curve for each day and how it grows.

7. ### Daily Cost ###
Chart showing how must RACA are spent each day.

## Configuration ##
---
![Configuration view](https://github.com/SpecificGuy/egg-simulator/blob/main/docs/Configuration.png?raw=true)*Configuration panel with default values.*

The configuration panel let the user parametrize the simulation as he intend to do. It is divided by 4 main sections plus a Simulation button on the bottom of the screen:

### Starting Account State ###
---
As the section suggest, this section must be populated by the user providing the actual state of his account. 
This configuration is used by the simulation as the first simulated day.

*No simulation is executed by the program on the first day.
In other words, the data provided is considered at the end of the day and with the matches of the game already played.*

Keep this in mind if you plan to execute a short term simulation.

In detail:

1. Raca Balance - The actual account balance or the amount the user intend to invest in the game.
2. Fragment Balance - The actual fragment balance of the account.
3. Potion Balance - The actual potion balance of the account. Yellow, Purple and Black diamond are missing but they will be provided as configuration in the future software updates
4. Egg Balance - The actual eggs count owned
5. Raca Total Revenue - Already gained quantity on RACA sold to the market and converted in real revenue

6. Metamons - The panel above is very similar to the one already documented in [Simulation data](#simulation-data). The user can insert all the metamons owned and set them for their Rarity, Level and Experience.

### Global Rates ###
---
This section contains all the data already in the game which is not supposed to change. In any case:

1. Match Limit - Match allowed for each metamon in each day.
2. Egg Fragment Quantity - Fragment quantity needed for minting an egg in the game

### Simulation parameters ###
---
This section contains all the parameters intended to be used for adjusting the simulation and test strategies. In particular

1.  Minimum Raca Revenue - Minimum amount of RACA the user want to withdraw from the account when available. The formula for the Revenue withdraw is the following one:

    `DAILY_REVENUE = RACA_BALANCE - POTION_COST_NEXT_DAY - MATCH_COST_NEXT_DAY - MINIMUM_RACA_REVENUE`

    If this formula return a quantity > 0, it is converted in Revenue for the account. This is because the simulation takes into account that some RACAs must be present in the account to play the next day matches and to level up all the metamons (i.e. Metamons under level 60).

2. Revenue start on metamon count
This parameter is used by the simulation to understand when the Revenue withdraw must start. If a user intend to reach a certain amount of metamons before starting withdrawing from the account, that's the parameter for him.

3. Potion buying on metamon count
This parameters is used by the simulation to understand when start buying potions and start levelling up metamons based on metamons owned.

4. Simulation Days
Number of simulate days

5. Metamon Auto Buy
The simulation auto buy metamons whenver it can, based on the RACA balance available during the day. The user can disable this option by clicking on the checkbox and can also select the rarity autobought from the simulation.

6. Auto Mint Eggs
The simulation auto mint eggs whenever it can.

7. Auto Sell Eggs
The simulation sells every egg whenever it can.

8. Auto buy potions
The simulation auto by potions everytime a metamon reach the required experience to level up. 

9. Auto level up
The simulation auto level up metamons when a metamon reach the required experience to level up.

### Prices ###
---
Prices of the market kept into consideration for the simulation and manual operations.
### Simulation button ###
---
By pressing the button simulation the parameters inserted into the configuration panel are used to simulate starting from the beginning up to the day configured.

**Beware that this functionality execute a totally new simulation, overriding completely the old one.**