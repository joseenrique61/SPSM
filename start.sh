#!/bin/bash

# Ensure the script stops if something fails
set -e

# Load environment variables from the .env file
echo "INFO: Loading variables from the .env file..."
if [ -f .env ]; then 
export $(grep -v '^#' .env | xargs)
else 
echo "WARNING: .env file not found. Default values will be used if they exist."
fi

# Validate that the necessary variables are present
: "${RABBITMQ_DEFAULT_USER?ERROR: The variable RABBITMQ_DEFAULT_USER is not defined in .env}"
: "${RABBITMQ_DEFAULT_PASSWORD?ERROR: The variable RABBITMQ_DEFAULT_PASSWORD is not defined in .env}"
: "${JWT_ISSUER?ERROR: The variable JWT_ISSUER is not defined in .env}"

# Generate password hash if credentials have changed
bash ./src/RabbitMQ/generate_password_hash.sh $RABBITMQ_DEFAULT_USER $RABBITMQ_DEFAULT_PASSWORD

# Generate private and public key if they don't exist yet
bash ./src/KongGateway/create_kong_file.sh $JWT_ISSUER

# Start the services with Docker Compose
echo "INFO: Starting all services with docker-compose..."
docker compose -p sps up --build "$@" 
