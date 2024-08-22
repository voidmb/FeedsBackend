const { app } = require('@azure/functions');
const axios = require('axios');
const jwt = require('jsonwebtoken');

const API_URL = 'https://localhost:44335'; // Reemplaza con la URL de tu API
const JWT_SECRET = '3e7f5e6a0b2d13a1c9bfe8d9a3f7b0c6d4e2a8f7a4c1d1e6e3d5f9b8a2c4a7e1f'; // Reemplaza con tu secreto JWT

app.http('FeedServiceFunction', {
    methods: ['GET', 'POST', 'PUT', 'DELETE', 'PATCH'],
    authLevel: 'anonymous', // Cambia esto si necesitas autenticación a nivel de Azure Function
    handler: async (request, context) => {

        const token = request.headers.get('Authorization')?.split(' ')[1];

        if (!token) {
            return {
                status: 401,
                body: 'No hay token de autorización'
            };
        }

        try {
            // Verificar el JWT
            jwt.verify(token, JWT_SECRET);

            const apiUrl = API_URL;
            const { method, url, query, body } = request;

            switch (method) {
                case 'GET':
                    if (url.includes('/feeds/')) {
                        // Obtener detalle de un feed
                        const id = url.split('/feeds/')[1];
                        const response = await axios.get(`${apiUrl}/feeds/${id}`);
                        return {
                            status: response.status,
                            body: response.data
                        };
                    } else {
                        // Obtener lista de feeds
                        const response = await axios.get(apiUrl, { params: query });
                        return {
                            status: response.status,
                            body: response.data
                        };
                    }

                case 'POST':
                    // Crear un nuevo feed
                    const postResponse = await axios.post(apiUrl, body, {
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    });
                    return {
                        status: postResponse.status,
                        body: postResponse.data
                    };

                case 'PUT':
                    // Actualizar un feed existente
                    const putId = url.split('/feeds/')[1];
                    const putResponse = await axios.put(`${apiUrl}/feeds/${putId}`, body);
                    return {
                        status: putResponse.status,
                        body: putResponse.data
                    };

                case 'DELETE':
                    // Eliminar un feed
                    const deleteId = url.split('/feeds/')[1];
                    const deleteResponse = await axios.delete(`${apiUrl}/feeds/${deleteId}`);
                    return {
                        status: deleteResponse.status,
                        body: deleteResponse.data
                    };

                case 'PATCH':
                    // Eliminar topics de un feed
                    const patchId = url.split('/feeds/')[1];
                    const patchResponse = await axios.patch(`${apiUrl}/feeds/${patchId}/topics`, body);
                    return {
                        status: patchResponse.status,
                        body: patchResponse.data
                    };

                default:
                    return {
                        status: 405,
                        body: 'Método no permitido'
                    };
            }
        } catch (error) {
            context.log.error(`Error: ${error.message}`);
            return {
                status: 401,
                body: 'Token invalido o expirado'
            };
        }
    }
});
