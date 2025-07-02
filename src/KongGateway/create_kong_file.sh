if [ -z "$JWT_ISSUER" ]; then
echo "ERROR: The issuer variable is empty."
exit 1
fi

cd ./src/UserService/UserService
# Check if the private.pem file exists
if [ ! -f "./private.pem" ]; then
echo "INFO: Generating private key"
openssl genrsa -out private.pem 2048
sudo chmod 644 private.pem
fi

# Generate public key
openssl rsa -in private.pem -pubout -out public.pem

export PUBLIC_KEY=$(sed 's/^/          /' public.pem)
rm public.pem

cd ../../KongGateway
# Create the kong.yaml file from the template
TEMPLATE_FILE="./config/kong.template.yaml"
OUTPUT_FILE="./config/kong.yaml"

echo "INFO: Creating $OUTPUT_FILE from the template..."
cp "$TEMPLATE_FILE" "$OUTPUT_FILE"

envsubst < $TEMPLATE_FILE > $OUTPUT_FILE

echo "INFO: $OUTPUT_FILE created and ready."
