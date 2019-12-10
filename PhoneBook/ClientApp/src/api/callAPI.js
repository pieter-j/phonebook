class CallAPI {

	call = async  (url, options) => {
		let result = null;
		let response = null;
		if (!url.startsWith('http')) {
			//url = 'https://batterywizard.azurewebsites.net/api/' + url;
			//url = 'https://localhost:5001/api/' + url;
			url = '/api/' + url;
		}
		try {

			response = await fetch(
				url,
				{
					...options,
					mode: 'cors',
					headers: {
						'Content-Type': 'application/json'
						// 'Content-Type': 'application/x-www-form-urlencoded',
					},
					redirect: 'follow', // manual, *follow, error
					referrer: 'no-referrer', // no-referrer, *client
				});

		} catch (ex) {
			throw new Error('Fetch network Exception');
		}
		if (response && response.ok) {
			if (response.status === 204) {
				result = null;
			} else {
				result = await response.json();
			}
		} else {
			if (response) {
				throw new Error(response.status + ' ' + response.statusText);
			} else {
				throw new Error('The server is not responding');
			}
		}
		return result;
	};

	get = async (url, options) => {
		return await this.call(url, options);
	};

	// options contains body with JSON.stringified data
	put = async (url, options) => {
		options = Object.assign({
			method: 'PUT', // *GET, POST, PUT, DELETE, etc.
		}, options)
		return await this.call(url, options);
	};

	// options contains body with JSON.stringified data
	post = async (url, options) => {
		options = Object.assign({
			method: 'POST', // *GET, POST, PUT, DELETE, etc.
		}, options)
		return await this.call(url, options);
	};

	delete = async (url, options) => {
		options = Object.assign({
			method: 'DELETE', // *GET, POST, PUT, DELETE, etc.
		}, options)
		return await this.call(url, options);
	};
	
	callAction = async (url, options, callFunction, successObject, errorObject, errorText, dispatch, growl) => {
		try {
			let result = await callFunction(url, options);
			dispatch({ ...successObject, result: result });
			return { result, error: null };
		} catch (ex) {
			console.log('Callaction ex', ex);
			let error = new Error(errorText);
			error.innerError = ex;
			dispatch({ ...errorObject, error: error });
			if (growl) {
				growl.show({
					sticky: true, closable: true, severity: 'error', detail: error.message
				});
			}
			return { result: null, error };
		}
	}
}

export default new CallAPI();