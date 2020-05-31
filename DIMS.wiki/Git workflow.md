### Git workflow:
#### Hot to name a branch:
> **`your-name/feature`**

#### How to name a commit:
> Bad: "I added some features to my controller"
> 
> **`Good: "Add GetUser action to UsersController"`**

#### Daily working process:
```
git checkout <name of your branch>
git pull --rebase origin <name of origin branch>
(stash you shanges if you will be asked to do before pull and resolve conflicts if need and don't forget to apply your stash again)	
git checkout -b <your-branch-name>
git status
git add . / git add "<name-of-file>"
git commit -m "<your-commit>"
git push -u origin <name of your branch>  // -u or --set-upstream
git checkout dev
git pull --rebase origin <name of origin branch> 
(stash you shanges if you will be asked to do before pull and resolve conflicts if need and don't forget to apply your stash again)
git merge <name of your branch> dev
git branch -d <name of your branch> //delete your branch after successful work completion
git push
```
> [Git-guide](https://rogerdudler.github.io/git-guide/) - text guide to refresh you git knowledges step by step
> 
> [LearnGitBranching](https://learngitbranching.js.org) - improve your git branching skills online