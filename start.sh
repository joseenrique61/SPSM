#!/bin/bash

# Ensure the script stops if something fails
set -e

#1. Load environment variables from the .env file
echo "INFO: Loading variables from the .env file..."
if [ -f .env ]; then 
export $(grep -v '^#' .env | xargs)
else 
echo "WARNING: .env file not found. Default values ​​will be used if they exist."
fi

# Validate that the necessary variables are present
: "${MS_RABBITMQ_USER?ERROR: The variable MS_RABBITMQ_USER is not defined in .env}"
: "${MS_RABBITMQ_PASSWORD?ERROR: The variable MS_RABBITMQ_PASSWORD is not defined in .env}"

# --- START DEBUG SECTION ---
echo "DEBUG: The password read from the .env is: '$MS_RABBITMQ_PASSWORD'"
# --- END OF DEBUG SECTION ---

#2. Generate the RabbitMQ password hash
echo "INFO: Generating a hash for the RabbitMQ password..."
# We use --rm to have the container automatically removed after running the command.
#RABBITMQ_PASSWORD_HASH=$(docker run --rm rabbitmq:4.1.1-management rabbitmqctl hash_password "$MS_RABBITMQ_PASSWORD")
RABBITMQ_PASSWORD_HASH=$(docker run --rm rabbitmq:4.1.1-management rabbitmqctl hash_password "$MS_RABBITMQ_PASSWORD" | tail -n 1)

# --- BEGINNING OF DEBUG SECTION ---
echo "DEBUG: The value captured in the RABBITMQ_PASSWORD_HASH variable is: '$RABBITMQ_PASSWORD_HASH'"
# --- END OF DEBUG SECTION ---

if [ -z "$RABBITMQ_PASSWORD_HASH" ]; then
echo "ERROR: The hash variable is empty. The 'docker run' command probably failed."
exit 1
fi
echo "INFO: Hash generated successfully."

#3. Create the definitions.json file from the template
TEMPLATE_FILE="./rabbitmq/definitions.template.json"
OUTPUT_FILE="./rabbitmq/definitions.json"

echo "INFO: Creating $OUTPUT_FILE from the template..."
cp "$TEMPLATE_FILE" "$OUTPUT_FILE"

#4. Replace the placeholders. We use # as a delimiter for security.
sed -i.bak "s#_RABBITMQ_USERNAME__#$MS_RABBITMQ_USER#g" "$OUTPUT_FILE"
sed -i.bak "s#__RABBITMQ_PASSWORD_HASH__#$RABBITMQ_PASSWORD_HASH#g" "$OUTPUT_FILE"
rm "${OUTPUT_FILE}.bak"

echo "INFO: $OUTPUT_FILE created and ready."

#5. Start the services with Docker Compose
echo "INFO: Starting all services with docker-compose..."
docker compose up "$@"