#!/bin/bash

# Install Gremlin Server
gremlinServerVersion="3.2.4"
wget --no-check-certificate -O $HOME/apache-tinkerpop-gremlin-server-$gremlinServerVersion-bin.zip http://archive.apache.org/dist/tinkerpop/$gremlinServerVersion/apache-tinkerpop-gremlin-server-$gremlinServerVersion-bin.zip
unzip $HOME/apache-tinkerpop-gremlin-server-$gremlinServerVersion-bin.zip -d $HOME/

# Start Gremlin Server
cd $HOME/apache-tinkerpop-gremlin-server-$gremlinServerVersion
bin/gremlin-server.sh conf/gremlin-server-modern.yaml > /dev/null 2>&1 &
cd $TRAVIS_BUILD_DIR