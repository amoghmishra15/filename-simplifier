import json
import os

# --------------------------------------------------------------------------------------------------
#   Terminate the program on invalid input
# --------------------------------------------------------------------------------------------------
def terminate():
	print('\nInvalid input. Terminating the program. No changes were made.')
	print('----------------------------------------------')
	input('Press [Enter] to exit...')
	exit()


# --------------------------------------------------------------------------------------------------
#   Change setting function
# --------------------------------------------------------------------------------------------------
def changeSettings():
	#   Greeting
	os.system('cls' if os.name == 'nt' else 'clear')
	print('\nConfigure settings')
	print('------------------\n')

	#   Open config file
	with open('config.json') as f:
		config = json.load(f)

	#   Print list of extensions available
	print('Extensions available')
	print('--------------------')
	for ext in config['extensions']:
		print('{} Extensions: {}'.format(ext['type'], ext['value']))

	#   Extension selection
	print('\nSelect extensions to work on')
	print('----------------------------')
	print('0 = Video extensions')
	print('1 = Audio extensions')
	print('2 = Document extensions')
	print('3 = Custom extensions. NOTE: you can edit them in the next prompt.')
	print('4 = Everything (including folders)')

	userIn = str(input('Your selection: '))
	if userIn in ('0', '1', '2', '3', '4'):
		config['flags'][0]['selectedExtension'] = userIn
	else:
		terminate()

	#   Prompt to edit custom extensions
	userIn = input('\nEdit custom extensions? (y/n): ')
	if userIn.lower() == 'y':

		print('Enter the new custom extensions in the given format. This list is caSe InsensiTive.')
		print('Note on syntax: each extension starts with a dot. Use space to separate extensions. Here\'s an example:')
		print(".custom1 .custom2 .custom3")

		userIn = input()
		if userIn[0] != ".":
			terminate()
		else:
			config['extensions'][3]['value'] = userIn
			# Print new custom ext
			print('\nUpdated successfully.\nCustom extensions: {}'.format(config['extensions'][3]['value']))

	#   Function convert y/n to true/false
	def tfSelector(boolIn):
		if boolIn == 'y':
			return True
		elif boolIn == 'n':
			return False
		else:
			print('Invalid input detected. Defaulting to yes.')
			return True

	#   Select other flags
	print('\nSet misc flags')
	print('--------------')

	#   Remove []
	userIn = input('1. Remove [everything inside brackets]? (y/n): ').lower()
	config['flags'][0]['rmSquareBracket'] = tfSelector(userIn)

	#   Remove ()
	userIn = input('2. Remove (everything inside parentheses)? (y/n): ').lower()
	config['flags'][0]['rmParentheses'] = tfSelector(userIn)

	#   Remove -
	userIn = input('3. Remove-dashes-between-words? (y/n): ').lower()
	config['flags'][0]['rmDash'] = tfSelector(userIn)

	#   Remove foreign char
	userIn = input('4. Remove non-English characters? (y/n): ').lower()
	config['flags'][0]['rmForeign'] = tfSelector(userIn)

	#   Enable title case
	userIn = input('5. Use title case (eg: reZero season 2 -> reZero Season 2)? (y/n): ').lower()
	config['flags'][0]['enTitleCase'] = tfSelector(userIn)

	#   Blacklisted words
	print('\nList of blacklisted words')
	print('-------------------------')

	#   Print default blacklisted words
	print('List of default blacklisted words: {}'.format(config['defaultBlacklist']))
	userIn = input('Activate default blacklist? (y/n): ').lower()
	config['flags'][0]['enDefaultBlacklist'] = tfSelector(userIn)

	#   Print custom blacklisted words
	print('\nList of custom blacklisted words: {}'.format(config['customBlacklist']))

	userIn = input('Edit custom blacklist? (y/n): ')
	if userIn.lower() == 'y':

		print('Enter the new custom blacklist in the given format')
		print('Note on syntax: separate each word with a space. This list is CasE InsEnsitive Here\'s an example:')
		print("alpha gamma delta")

		config['customBlacklist'] = input()
		# Print new custom ext
		print('\nUpdated successfully.\nCustom blacklist: {}'.format(config['customBlacklist']))
		print('\nA note on blacklist: every single instance of the blacklisted word will be removed.')
		print('Say you blacklisted `WEB`. If a filename has `website` in it\'s name, it will become just `site`.')
		input('Press [Enter] to continue...')

	userIn = input('Activate custom blacklist? (y/n): ').lower()
	config['flags'][0]['enCustomBlacklist'] = tfSelector(userIn)

	#   Update config
	with open('config.json', 'w') as f:
		json.dump(config, f, indent=4)

