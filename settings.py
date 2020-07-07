import json
import os

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

	#   Prompt to edit custom extensions
	userIn = input('\nEdit custom extensions? (y/n): ')
	if userIn.lower() == 'y':

		print('Enter the new custom extensions in the given format')
		print("'.custom1', '.custom2', '.custom3'")

		userIn = input()
		if (userIn[0] != "'") or (userIn[-1] != "'"):
			print('Invalid Syntax. Terminating program.')
			quit()
		else:
			config['extensions'][3]['value'] = userIn
			# Print new custom ext
			print('\nUpdated successfully.\nCustom extensions: {}'.format(config['extensions'][3]['value']))

	#   Extension selection
	print('\nSelect extensions to work on')
	print('----------------------------')
	print('0 = Video extensions')
	print('1 = Audio extensions')
	print('2 = Document extensions')
	print('3 = Custom extensions')

	userIn = str(input('Your selection: '))
	if userIn in ('0', '1', '2', '3', '4'):
		config['flags'][0]['selectedExtension'] = userIn
	else:
		print('Invalid input. Terminating program')
		quit()

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

	#   Remove foreign (non-ASCII) characters
	userIn = input('1. Remove non-English characters? (y/n): ').lower()
	config['flags'][0]['rmForeign'] = tfSelector(userIn)

	#   Remove dots
	userIn = input('2. Remove dot separator? (y/n): ').lower()
	config['flags'][0]['rmDot'] = tfSelector(userIn)

	#   Remove underscore
	userIn = input('3. Remove underscore separators? (y/n): ').lower()
	config['flags'][0]['rmUnderscore'] = tfSelector(userIn)

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
		print("'blacklistedWord1', 'blacklistedWord2'")

		userIn = input()
		if (userIn[0] != "'") or (userIn[-1] != "'"):
			print('Invalid Syntax. Terminating program.')
			quit()
		else:
			config['customBlacklist'] = userIn
			# Print new custom ext
			print('\nUpdated successfully.\nCustom blacklist: {}'.format(config['customBlacklist']))

	userIn = input('Activate custom blacklist? (y/n): ').lower()
	config['flags'][0]['enCustomBlacklist'] = tfSelector(userIn)

	#   Update config
	with open('config.json', 'w') as f:
		json.dump(config, f, indent=2)

