import os
import re
import json
from settings import changeSettings

#   Clear terminal
def clearTerminal():
	os.system('cls' if os.name == 'nt' else 'clear')

#   Load settings
with open('config.json') as f:
	config = json.load(f)

#   Print and load settings
clearTerminal()
print('\nFilename Simplifier')
print('----------------------------')

selectedExtensions = tuple(eval(config['extensions'][int(config['flags'][0]['selectedExtension'])]['value']))
print('Extensions to rename: {}'.format(selectedExtensions))

rmForeign = config['flags'][0]['rmForeign']
print('Remove non-English characters: {}'.format(rmForeign))

rmDot = config['flags'][0]['rmDot']
print('Remove dot separator: {}'.format(rmDot))

rmUnderscore = config['flags'][0]['rmUnderscore']
print('Remove underscore separator: {}'.format(rmUnderscore))

defaultBlacklist = tuple(eval(config['defaultBlacklist']))
print('\nDefault blacklisted words: {}'.format(defaultBlacklist))
enDefaultBlacklist = config['flags'][0]['enDefaultBlacklist']
print('Default blacklist removal enabled: {}'.format(enDefaultBlacklist))

customBlacklist = tuple(eval(config['customBlacklist']))
print('\nCustom blacklisted words: {}'.format(customBlacklist))
enCustomBlacklist = config['flags'][0]['enCustomBlacklist']
print('Custom blacklist enabled: {}'.format(enCustomBlacklist))


#   Edit settings prompt
print('\nSettings loaded successfully')
print('----------------------------')
userIn = input('Edit settings? (y/n): ')
if userIn.lower() == 'y':
	changeSettings()
	clearTerminal()
	print('Settings updated')
	print('----------------')


#   Terminate the program on invalid input
def terminate():
	print('\nTerminating the program. No changes were made.')
	print('----------------------------------------------')
	quit()


#   User input: working directory
clearTerminal()
print('Enter the full directory/path of the folder which contains files to be renamed')
rnDir = input('dir: ')

#   Info prompt
clearTerminal()
print('\nDesalinate will rename all files ending with {} in "{}"'.format(selectedExtensions, rnDir))

#   Change to working directory and list files
os.chdir(rnDir)
print('\nFollowing files will be affected')
print('--------------------------------')

for f in os.listdir():
	fExt = f
	if fExt.endswith(selectedExtensions):
		print(fExt)

#   User input: confirm working directory
prompt = input('\nContinue? (y/n): ')
if prompt.lower() == 'y':
	print('', end='')
else:
	terminate()

# Core function
regexBrackets = regex = r'\[(.*?)\]'

def desalinate(mode):
	for file in os.listdir():
		newF = file
		if newF.endswith(selectedExtensions):

			# Remove everything in brackets + '[]'
			newF = re.sub(regexBrackets, "", newF)
			# Separate the last word from extension "xyz abc.ext" -> "xyz abc .ext"
			newF = newF.split('.')
			newF[0] += ' '
			newF = '.'.join(newF)

			# Remove non-English characters
			if rmForeign is True:
				newF = newF.encode("ascii", errors="ignore").decode()

			# Remove default blacklisted words
			queryWords = newF.split()
			resultWords = [word for word in queryWords if word.lower() not in defaultBlacklist]
			newF = ' '.join(resultWords)

			# Remove custom blacklisted words
			queryWords = newF.split()
			resultWords = [word for word in queryWords if word.lower() not in customBlacklist]
			newF = ' '.join(resultWords)

			# Remove extra whitespace
			newF = re.sub(' +', ' ', newF)
			newF = newF.replace(' .', '.')
			newF = newF.replace('( ', '(')
			newF = newF.replace(') ', ')')
			newF = newF.strip()

			if mode is False:
				print(newF)
			else:
				os.rename(file, newF)


#   Preview new filenames
print('\nPreview of renames')
print('------------------')
workingMode = False  # preview mode
desalinate(workingMode)

#   User input: final confirmation
print('\nImportant note')
print('--------------')
print('There is no backup in this program.\nOnce you approve the renames, they cannot be reverted.')
prompt = input('Finalize all renames? (y/n): ')

if prompt.lower() == 'y':
	print('Finalizing...\n')
	workingMode = True  # finalize mode
	desalinate(workingMode)
else:
	terminate()

input("Done. Files have been renamed successfully.\nPress[Enter] to exit the program...")
