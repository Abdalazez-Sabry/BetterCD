# BetterCD

A command line tool I have created to make it easier for me to navigate through directories with terminal user interface.


## How to install it:
* Save the files on any directory you want.
* Add the this directory to your environment variables
* to use it in powershell write this script on your $profile file


    ```bash
    function bcd {
       $bcddirectorey = "C:\Dev\BetterCd\.bcddirectorey"
       bettercd $bcddirectorey 
       cd (get-content $bcddirectorey)
    }
    ```

That's it, just type **bcd** in your powershell to start navigating.

## Shortcuts:
* Arrow up or k -> go up
* Arrow down or j -> go down
* Enter or l -> open directory or file
* first item ( **..** ) or h -> go back
* s -> cd to current directory
* esc -> cancel ( stay on the same directory you ran the command from )
* ctr + q -> force quit

## Notes:
at the moment it works for windows
