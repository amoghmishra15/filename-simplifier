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


#   Terminate the program on invalid input
def terminate():
	print('\nTerminating the program. No changes were made.')
	print('----------------------------------------------')
	quit()


#   Load settings
extNumber = int(config['flags'][0]['selectedExtension'])
selectedExtensions = tuple(eval(config['extensions'][extNumber]['value']))
rmForeign = config['flags'][0]['rmForeign']
defaultBlacklist = tuple(eval(config['defaultBlacklist']))
enDefaultBlacklist = config['flags'][0]['enDefaultBlacklist']
customBlacklist = tuple(eval(config['customBlacklist']))
enCustomBlacklist = config['flags'][0]['enCustomBlacklist']
enTitleCase = config['flags'][0]['enTitleCase']

#   Print settings
clearTerminal()
print('\nFilename Simplifier')
print('----------------------------')

print('Simplify filenames ending with: {}'.format(selectedExtensions))
if enCustomBlacklist is True:
	print('Custom blacklisted words: {}'.format(customBlacklist))

print('\nRemove non-English characters: {}'.format(rmForeign))
print('Capitalize each word: {}'.format(enTitleCase))
print('Default blacklisted words removal enabled: {}'.format(enDefaultBlacklist))
print('Custom blacklisted words removal enabled: {}'.format(enCustomBlacklist))

#   Edit settings prompt
print('\nSettings loaded successfully')
print('----------------------------')
userIn = input('Edit settings? (y/n): ')
if userIn.lower() == 'y':
	changeSettings()
	clearTerminal()
	print('Settings updated')
	print('----------------')
	print('Please rerun the program')
	quit()

#   User input: working directory
clearTerminal()
print('Enter the full directory/path of the folder which contains files to be renamed')
rnDir = input('dir: ')

#   Info prompt
clearTerminal()
print('\nProcessing all files ending with {} in "{}"'.format(selectedExtensions, rnDir))

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

regexBrackets = r'\[(.*?)\]'  # [*]
regexBitrate = r'(?i)\d\d\dkbps'  # 320kbps
regexUnderscore = r'_'

# NOTE: program calls A->B->C->D. If, say, typeB is called first, it will break the regex for typeA.
regexResolutionTypeB = r'(?i)\d\d\dp'  # 720p
regexResolutionTypeA = r'(?i)\d\d\d\dp'  # 1080p
regexResolutionTypeD = r'(?i)\d\d\dx\d\d\d'  # 720x480
regexResolutionTypeC = r'(?i)\d\d\d\dx\d\d\d\d'  # 1920x1080

# Misc
allRenames = []                         # Store all rename previews. Used to check for duplicates.

# Core function
def desalinate(mode):
	for file in os.listdir():
		newF = file
		if newF.endswith(selectedExtensions):

			# Regex removers
			newF = re.sub(regexBrackets, " ", newF)
			newF = re.sub(regexBitrate, " ", newF)
			newF = re.sub(regexUnderscore, " ", newF)
			newF = re.sub(regexResolutionTypeA, " ", newF)
			newF = re.sub(regexResolutionTypeB, " ", newF)
			newF = re.sub(regexResolutionTypeC, " ", newF)
			newF = re.sub(regexResolutionTypeD, " ", newF)

			# Remove dots + "xyz abc.ext" -> "xyz abc .ext"
			newF = newF.split('.')
			newF[-1] = " ." + newF[-1]
			newF = ' '.join(newF)

			# Remove non-English characters
			if rmForeign is True:
				newF = newF.encode("ascii", errors="ignore").decode()

			# Default blacklist
			for index, words in enumerate(defaultBlacklist):
				regexBlacklist = "(?i)" + defaultBlacklist[index]
				newF = re.sub(regexBlacklist, " ", newF)

			# Custom blacklist
			for index, words in enumerate(customBlacklist):
				regexBlacklist = "(?i)" + customBlacklist[index]
				newF = re.sub(regexBlacklist, " ", newF)

			# Remove extra whitespace
			newF = newF.replace('( )', '')
			newF = newF.replace('()', '')
			newF = newF.replace('( ', '(')
			newF = newF.replace(' )', ')')
			newF = re.sub(' +', ' ', newF)
			newF = newF.replace(' .', '.')
			newF = newF.strip()

			# Title case
			if enTitleCase is True:
				newF = newF.split('.')
				newF[0] = newF[0].title()
				newF = '.'.join(newF)

			if mode is False:
				print(newF)
				allRenames.append(newF)  # Safeguard for duplicates
			else:
				os.rename(file, newF)


#   Preview new filenames
print('\nPreview of renames')
print('------------------')
workingMode = False  # preview mode
desalinate(workingMode)

# Safeguard for duplicates
if len(allRenames) != len(set(allRenames)):
	print('\n\nFatal error')
	print('-----------')
	print('Two or more files end up with the same name upon simplification. This will result in overwrites.')
	print('Please add episode numbers in them manually.')
	print('You cannot proceed until you rename the conflicting files manually.')
	terminate()

#   User input: final confirmation
print('\nFinalize')
print('--------')
print('All renamed files have been validated against overwrite conflicts.')
print('However, there is no backup in this program. Once you approve the renames, they cannot be reverted.')
prompt = input('Finalize all renames? (y/n): ')

if prompt.lower() == 'y':
	print('Finalizing...\n')
	workingMode = True  # finalize mode
	desalinate(workingMode)
else:
	terminate()

input("Done. Files have been renamed successfully.\nPress[Enter] to exit the program...")
