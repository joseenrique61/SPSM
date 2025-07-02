#!/bin/bash

kong_yaml="compose/config/kong.yaml"
docker exec sps-kong-1 kong config parse /opt/kong/kong.yaml \
  && docker exec sps-kong-1 kong reload
