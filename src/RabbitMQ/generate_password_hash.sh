RABBITMQ_DEFAULT_USER=$1
RABBITMQ_DEFAULT_PASSWORD=$2

if [ -z "$RABBITMQ_DEFAULT_USER" ]; then
echo "ERROR: The user variable is empty."
exit 1
fi

if [ -z "$RABBITMQ_DEFAULT_PASSWORD" ]; then
echo "ERROR: The password variable is empty."
exit 1
fi

#1. Check if credentials are the same
CREDENTIALS_FILE="./src/RabbitMQ/last_credentials.env"
if [ -f "$CREDENTIALS_FILE" ]; then
  source "$CREDENTIALS_FILE"

  if [ "$RABBITMQ_DEFAULT_USER" == "$LAST_USER" ] && [ "$RABBITMQ_DEFAULT_PASSWORD" == "$LAST_PASS" ]; then
    echo "INFO: Credentials have not changed. Exiting."
    exit 0
  fi
fi

#2. Generate the RabbitMQ password hash
echo "INFO: Generating a hash for the RabbitMQ password..."
# We use --rm to have the container automatically removed after running the command.
RABBITMQ_PASSWORD_HASH=$(docker run --rm rabbitmq:4.1.1-management rabbitmqctl hash_password "$RABBITMQ_DEFAULT_PASSWORD" | tail -n 1)

if [ -z "$RABBITMQ_PASSWORD_HASH" ]; then
echo "ERROR: The hash variable is empty. The 'docker run' command probably failed."
exit 1
fi
echo "INFO: Hash generated successfully."

#3. Create the definitions.json file from the template
TEMPLATE_FILE="./src/RabbitMQ/definitions.template.json"
OUTPUT_FILE="./src/RabbitMQ/definitions.json"

echo "INFO: Creating $OUTPUT_FILE from the template..."
cp "$TEMPLATE_FILE" "$OUTPUT_FILE"

#4. Replace the placeholders. We use # as a delimiter for security.
sed -i.bak "s#__RABBITMQ_USERNAME__#$RABBITMQ_DEFAULT_USER#g" "$OUTPUT_FILE"
sed -i.bak "s#__RABBITMQ_PASSWORD_HASH__#$RABBITMQ_PASSWORD_HASH#g" "$OUTPUT_FILE"
rm "${OUTPUT_FILE}.bak"

#5. Update credentials
echo "LAST_USER=$RABBITMQ_DEFAULT_USER" > "$CREDENTIALS_FILE"
echo "LAST_PASS=$RABBITMQ_DEFAULT_PASSWORD" >> "$CREDENTIALS_FILE"

echo "INFO: $OUTPUT_FILE created and ready."

