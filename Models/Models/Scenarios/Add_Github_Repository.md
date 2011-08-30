Use case name
==============
Add Github Repository

Summary
========
User selects Github Repository. System imports Repository files.

Actor
=====
User

Preconditions
=============
User has started the System.

Main sequence
==============
1. User enters URL of Github Repository into System
2. System imports Github Repository Sourcefiles

Extensions
==========
1* Github Repository doesnt exist
1a. System displays error and cancels import

2* Github Repository has been imported already
1a. System cancels Import and informs User

2* At any point, Github Repository Import fails
2a. System aborts Import and informs User

Postcondition
==============
System has imported Github Repository.
