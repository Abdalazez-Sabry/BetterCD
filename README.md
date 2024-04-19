# BetterCD

A command line tool I have created to make it easier for me to navigate through directories with terminal user interface.


## How to install it:
* Save the files on any directory you want.
* Add the this directory to your environment variables
* To use it in powershell write this script on your $profile file


    ```bash
    function bcd {
       $bcddirectorey = "<BetterCD Path>\.bcddirectorey"
       bettercd $bcddirectorey 
       cd (get-content $bcddirectorey)
    }
    ```

That's it, just type **bcd** in your powershell to start navigating.

## Shortcuts:
* Arrow up or k -> go up
* Arrow down or j -> go down
* Enter or l -> open directory or file
* First item ( **..** ) or h -> go back
* s -> cd to current directory
* Esc -> cancel ( stay on the same directory you ran the command from )
* Ctr + q -> force quit

## Notes:
At the moment it works for windows
