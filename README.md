**Bus Ticketing System**

![](image/image.001.png)

American International University- Bangladesh




CSC 4261: Advanced Programming in .NET


~~Project Report~~

Summer 23-24



Project Title: <a name="_hlk126436574"></a>Bus Ticketing System

Group Number: 01

Section: B



|**Student Name**|**Student Id**|
| :-: | :-: |
|Monkir Chowdhury|20-44085-2|
|Forhad Ali Emon|20-44178-2|

**

**Introduction:**

The bus ticketing system is an essential component of public transportation that enables passengers to book and purchase bus tickets in advance. It is a web-based application that facilitates easy and convenient ticket booking and payment options for commuters. The system provides real-time information about bus routes, schedules, and ticket availability to ensure a seamless and hassle-free travel experience for passengers.

The bus ticketing system is designed to simplify the bus booking process by allowing passengers to book their tickets online from the comfort of their homes or offices. This eliminates the need for physical ticket counters and long queues, making the process more efficient and convenient for both passengers and bus operators.

**Problem Analysis**

Before developing a bus ticketing system, it is crucial to analyse the problems that currently exist in the existing system. Here are some of the issues faced by commuters and bus operators that a bus ticketing system could address:

1\.Inconvenient booking process

2\.Limited payment options

3\.Lack of real-time information

4\.Difficulty in canceling or changing bookings

5\.Limited tracking options

Feature Analysis:

1. User Category:

There are n-types of Users here. They are:

(4 users)

- Admin
- Passenger
- Employee
- Bus provider



1. Feature List:

the “**user**” has the following features:

- ✔️✔️ Login.
- ✔️✔️ Logout.
- ✔️✔️ Change password.
- ⏳Reset password(forgotten)
- ✔️✔️ See profile information.
- ✔️✔️ See notice.
- ✔️✔️✔️ Withdraw money.
- ✔️✔️✔️ Deposit money.

the “**Admin**” has the following features:

- ✔️✔️✔️ CRUD employee.
- ✔️✔️✔️ CRUD coupons.

the “**Employee**” has the following features:

- ✔️✔️✔️ CRUD notice.
- ✔️✔️✔️ CRUD place.
- ✔️✔️✔️ CRUD bus provider.
- ✔️✔️✔️Approve adding or cancelling a bus (request from bus provider)
- ~~Manage report/review~~.

the “**Customer**” has the following features:

- ✔️✔️✔️ Registration
- ✔️✔️✔️ See the available trip and discount coupon.
- ✔️✔️✔️ See the available seat of a trip.
- ✔️✔️✔️ Book ticket
- ✔️✔️✔️ Use coupons.
- ✔️✔️✔️ cancel (partial amount will be refund as policy)
- ✔️✔️✔️ pay ticket.
- ~~Report/review/get support~~.

the “**Bus Provider**” has the following features:

- ✔️✔️ ❓CRUD bus. 
- ✔️✔️✔️ Request to add a trip of a bus (cut 2000 taka from account) (need approval of an employee).
- ✔️✔️✔️ Undo adding trip request (return 2000 taka to account).
- ✔️✔️✔️ Request to cancel a trip of a bus (need approval of employee).
- ✔️✔️✔️ Undo cancelling trip request (return 2000 taka to account).
- ✔️✔️✔️ See the ticket status of trip.
- ~~See/replay report/review~~.


1. Design:

![](image/image.002.png)

![Diagram, schematic

Description automatically generated](image/image.003.png)

~~Figure: ER Diagram (old)~~

![Diagram

Description automatically generated](image/image.004.png)

~~Figure: Use Case Diagram (old)~~


**	
**


|![](image/image.005.png)|**American International University-Bangladesh (AIUB)**|
| :-: | :- |

