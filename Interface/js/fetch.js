async function fetchData(url, method, jwtToken, bodyData) {
    const headers = new Headers();
    
    if (method !== "GET") headers.append('Content-Type', 'application/json');

    if (jwtToken !== null) headers.append('Authorization', `Bearer ${jwtToken}`);

    const requestOptions = {
        method: method,
        headers: headers

    };

    if (method !== "GET") requestOptions.body = JSON.stringify(bodyData);
    else requestOptions.body = null;

    try {
        const response = await fetch(url, requestOptions);

        const isJson = response.headers.get('content-type')?.includes('application/json');

        // if (!response.ok) {
        //     throw new Error(`HTTP error! status: ${response.status}`);
        // }

        const data = isJson ? await response.json() : await response.text();

        data.statusCode = response.status;

        return data;
    } catch (error) {
        console.error('Fetch error:', error);
        throw error;
    }
}