Use case name
==============
Tag Sourcefile

Summary
========
User selects Sourcefile. User adds tag. System saves tag.

Actor
=====
User

Preconditions
=============
User has selected a Github Repository.

Main sequence
==============
1. User selects Sourcefile
2. User adds tag
3. System saves tag

Extensions
==========
2* Tag with same name is already assigned to Sourcefile
2a. System ignores request

Postcondition
==============
System has added tag to sourcefile.