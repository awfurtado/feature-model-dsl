<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="ebe8a96e-5b3d-4e67-9164-84bda2edccbd" Description="Design and configure feature models in Visual Studio. Check http://featuremodeldsl.codeplex.com for details." Name="FeatureModelDSL" DisplayName="FeatureModelDSL" Namespace="UFPE.FeatureModelDSL" ProductName="FeatureModelDSL" CompanyName="UFPE" PackageGuid="abe2bc90-2b20-4ee8-9922-9ebbd51bd3b3" PackageNamespace="UFPE.FeatureModelDSL" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="c00be157-2d50-4bde-93c9-71720797dce3" Description="Elements embedded in the model. Appear as boxes on the diagram." Name="Feature" DisplayName="Feature" Namespace="UFPE.FeatureModelDSL">
      <BaseClass>
        <DomainClassMoniker Name="FeatureModelElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="ace50e02-a56d-490f-a7c7-e9b6f17387be" Description="Feature name" Name="Name" DisplayName="Name" DefaultValue="" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3a666bef-9823-4d10-8cd1-814dbcfa60ae" Description="How often the feature occurs across multiple products of the domain" Name="Occurence" DisplayName="Occurence">
          <Type>
            <DomainEnumerationMoniker Name="Occurence" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6e22a125-c5f1-4b26-aaec-efa23d3b0d3b" Description="Feature description" Name="Description" DisplayName="Description">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6b461561-1836-4eff-aae5-954244228825" Description="Wheter this feature is defined in a feature model other than this one." Name="IsReference" DisplayName="Is Reference" Category="Cross-Model Reference">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="be55efca-d5c2-49e3-b49d-6aac32a66c5c" Description="If this feature is a reference, this property should be filled with the feature model file name containing the definition (for example, MainFeatureModel.fm). Leave this field blank otherwise." Name="DefinitionFeatureModelFile" DisplayName="Definition Feature Model Diagram" Category="Cross-Model Reference">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(UFPE.FeatureModelDSL.CustomTypeEditors.FeatureModelDiagramTypeEditor), typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="09e8ec53-03f6-4e0e-9b5d-b2d510a83dc8" Description="Description for UFPE.FeatureModelDSL.Alternative" Name="Alternative" DisplayName="Alternative" Namespace="UFPE.FeatureModelDSL">
      <BaseClass>
        <DomainClassMoniker Name="FeatureModelElement" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="8fe6588b-824c-4141-a08f-73edce8ed97f" Description="Minimum number of features associated to this alternative set that should be selected when configuring a product" Name="Min" DisplayName="Min" DefaultValue="1">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="86362966-0aec-46dd-a976-fa394d7fd9cc" Description="Maximum number of features associated to this alternative set that should be selected when configuring a product" Name="Max" DisplayName="Max" DefaultValue="1">
          <Type>
            <ExternalTypeMoniker Name="/System/Int32" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="465d46b6-466a-4606-8147-d124023892c7" Description="String visualizer for this Alternative's Min and Max values" Name="MinMaxIntervalText" DisplayName="Min Max Interval Text" Kind="Calculated" IsBrowsable="false">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="94f7d5e3-9d7d-4d04-9a53-99c6a90d0e7f" Description="Description for UFPE.FeatureModelDSL.FeatureModelElement" Name="FeatureModelElement" DisplayName="Feature Model Element" InheritanceModifier="Abstract" Namespace="UFPE.FeatureModelDSL" />
    <DomainClass Id="7596a99b-9ffc-48ba-9fff-a1f60b4f6849" Description="Description for UFPE.FeatureModelDSL.FeatureModel" Name="FeatureModel" DisplayName="Feature Model" Namespace="UFPE.FeatureModelDSL">
      <Properties>
        <DomainProperty Id="e31a6d2f-3b78-46ca-a9a3-fc1bfe3913d1" Description="Feature model name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3dffe632-c256-4bea-999f-8ace8944e7e3" Description="Feature model description" Name="Description" DisplayName="Description">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7b67eb71-3c4e-4270-b4bd-29b2c4354e48" Description="If this feature model is actually a detailing of a subset of a parent feature model, the parent feature model file should be specified here. Otherwise, this field shoud be left blank." Name="ParentFeatureModelFile" DisplayName="Parent Feature Model File">
          <Attributes>
            <ClrAttribute Name="System.ComponentModel.Editor">
              <Parameters>
                <AttributeParameter Value="typeof(UFPE.FeatureModelDSL.CustomTypeEditors.FeatureModelDiagramTypeEditor), typeof(System.Drawing.Design.UITypeEditor)" />
              </Parameters>
            </ClrAttribute>
          </Attributes>
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="FeatureModelElement" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>FeatureModelHasFeatureModelElements.FeatureModelElements</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="3ede3913-0adb-4eb7-ab7d-49c96e946ea9" Description="Description for UFPE.FeatureModelDSL.FeatureModelElementReferencesSubFeatureModelElements" Name="FeatureModelElementReferencesSubFeatureModelElements" DisplayName="Feature Model Element References Sub Feature Model Elements" Namespace="UFPE.FeatureModelDSL">
      <Source>
        <DomainRole Id="c3bafb13-5b7c-4627-a822-26fc6ded2228" Description="Description for UFPE.FeatureModelDSL.FeatureModelElementReferencesSubFeatureModelElements.SourceFeatureModelElement" Name="SourceFeatureModelElement" DisplayName="Source Feature Model Element" PropertyName="SubFeatureModelElements" PropertyDisplayName="Sub Feature Model Elements">
          <RolePlayer>
            <DomainClassMoniker Name="FeatureModelElement" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="8d425abd-7d53-4e3e-988e-5d855a565554" Description="Description for UFPE.FeatureModelDSL.FeatureModelElementReferencesSubFeatureModelElements.TargetFeatureModelElement" Name="TargetFeatureModelElement" DisplayName="Target Feature Model Element" PropertyName="ParentFeatureModelElement" Multiplicity="ZeroOne" PropertyDisplayName="Parent Feature Model Element">
          <RolePlayer>
            <DomainClassMoniker Name="FeatureModelElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="d7947b3c-6e0f-4b62-8634-bc18df843893" Description="Description for UFPE.FeatureModelDSL.Constrains" Name="Constrains" DisplayName="Constrains" Namespace="UFPE.FeatureModelDSL">
      <Properties>
        <DomainProperty Id="9a439ed0-5191-487f-92c4-3fe084325b0b" Description="Type of constraint implied by this relationship" Name="Constraint" DisplayName="Constraint" DefaultValue="Requires">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="24f0d74a-bc8c-4122-9d43-3a400a47b8e6" Description="Description for UFPE.FeatureModelDSL.Constrains.SourceFeature" Name="SourceFeature" DisplayName="Source Feature" PropertyName="ConstrainedFeatures" PropertyDisplayName="Constrained Features">
          <RolePlayer>
            <DomainClassMoniker Name="Feature" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="eefdccdd-6722-489a-8b91-274eef157daa" Description="Description for UFPE.FeatureModelDSL.Constrains.TargetFeature" Name="TargetFeature" DisplayName="Target Feature" PropertyName="ConstrainedBy" PropertyDisplayName="Constrained By">
          <RolePlayer>
            <DomainClassMoniker Name="Feature" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="33c9b855-3892-4651-b48c-9b0184c93424" Description="Description for UFPE.FeatureModelDSL.FeatureModelHasFeatureModelElements" Name="FeatureModelHasFeatureModelElements" DisplayName="Feature Model Has Feature Model Elements" Namespace="UFPE.FeatureModelDSL" IsEmbedding="true">
      <Source>
        <DomainRole Id="0a244800-a887-4208-9b82-259a87fbb4b5" Description="Description for UFPE.FeatureModelDSL.FeatureModelHasFeatureModelElements.FeatureModel" Name="FeatureModel" DisplayName="Feature Model" PropertyName="FeatureModelElements" Multiplicity="OneMany" PropertyDisplayName="Feature Model Elements">
          <RolePlayer>
            <DomainClassMoniker Name="FeatureModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="47eb23e4-89ee-4ed7-b722-e3cd75e477fb" Description="Description for UFPE.FeatureModelDSL.FeatureModelHasFeatureModelElements.FeatureModelElement" Name="FeatureModelElement" DisplayName="Feature Model Element" PropertyName="FeatureModel" Multiplicity="One" PropagatesDelete="true" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Feature Model">
          <RolePlayer>
            <DomainClassMoniker Name="FeatureModelElement" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <DomainEnumeration Name="Occurence" Namespace="UFPE.FeatureModelDSL" Description="How often should a feature appear in the domain products">
      <Literals>
        <EnumerationLiteral Description="The feature always occur in all product instances" Name="Mandatory" Value="0" />
        <EnumerationLiteral Description="The feature may or may not occur in a product instance" Name="Optional" Value="1" />
        <EnumerationLiteral Description="Occurence does not apply to the feature" Name="NotApply" Value="2">
          <Notes>Used for root domain concepts and features belonging to alternatives</Notes>
        </EnumerationLiteral>
      </Literals>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <GeometryShape Id="8f6e2d92-fb38-499f-94e8-52de295c433a" Description="Shape used to represent ExampleElements on a Diagram." Name="FeatureShape" DisplayName="Feature Shape" Namespace="UFPE.FeatureModelDSL" FixedTooltipText="Feature Shape" FillColor="255, 255, 128" OutlineColor="113, 111, 110" InitialHeight="0.4" OutlineThickness="0.01" FillGradientMode="Vertical" Geometry="Rectangle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="-0.1">
        <IconDecorator Name="MandatoryDecorator" DisplayName="Mandatory Decorator" DefaultIcon="Resources\MandatoryIcon.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="-0.1">
        <IconDecorator Name="OptionalDecorator" DisplayName="Optional Decorator" DefaultIcon="Resources\OptionalIcon.bmp" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerBottomLeft" HorizontalOffset="0" VerticalOffset="0">
        <IconDecorator Name="ReferenceDecorator" DisplayName="Reference Decorator" DefaultIcon="Resources\ReferenceIcon.bmp" />
      </ShapeHasDecorators>
    </GeometryShape>
    <ImageShape Id="b8e909e2-f92b-4005-a3b4-fa945882ce51" Description="Description for UFPE.FeatureModelDSL.AlternativeShape" Name="AlternativeShape" DisplayName="Alternative Shape" Namespace="UFPE.FeatureModelDSL" FixedTooltipText="Alternative Shape" InitialHeight="1" Image="Resources\AlternativeImage.bmp">
      <ShapeHasDecorators Position="OuterMiddleRight" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="MinMaxDecorator" DisplayName="Min Max Decorator" DefaultText="MinMaxDecorator" />
      </ShapeHasDecorators>
    </ImageShape>
  </Shapes>
  <Connectors>
    <Connector Id="e04ab2cb-305e-4486-b553-3f87e0362116" Description="" Name="ConnectConnector" DisplayName="Connect Connector" Namespace="UFPE.FeatureModelDSL" GeneratesDoubleDerived="true" FixedTooltipText="Connect Connector" Thickness="0.02" RoutingStyle="Straight" />
    <Connector Id="55757f59-39bb-41a9-bd54-24378a33aad5" Description="Description for UFPE.FeatureModelDSL.ConstraintConnector" Name="ConstraintConnector" DisplayName="Constraint Connector" Namespace="UFPE.FeatureModelDSL" FixedTooltipText="Constraint Connector" DashStyle="Dash" TargetEndStyle="EmptyArrow" Thickness="0.02">
      <ConnectorHasDecorators Position="SourceTop" OffsetFromShape="0" OffsetFromLine="0">
        <TextDecorator Name="ConstraintDecorator" DisplayName="Constraint Decorator" DefaultText="ConstraintDecorator" />
      </ConnectorHasDecorators>
    </Connector>
  </Connectors>
  <XmlSerializationBehavior Name="FeatureModelDSLSerializationBehavior" Namespace="UFPE.FeatureModelDSL">
    <ClassData>
      <XmlClassData TypeName="Feature" MonikerAttributeName="name" MonikerElementName="featureMoniker" ElementName="feature" MonikerTypeName="FeatureMoniker">
        <DomainClassMoniker Name="Feature" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="Feature/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="occurence">
            <DomainPropertyMoniker Name="Feature/Occurence" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="Feature/Description" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="constrainedFeatures">
            <DomainRelationshipMoniker Name="Constrains" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="isReference">
            <DomainPropertyMoniker Name="Feature/IsReference" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="definitionFeatureModelFile">
            <DomainPropertyMoniker Name="Feature/DefinitionFeatureModelFile" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="FeatureShape" MonikerAttributeName="" MonikerElementName="featureShapeMoniker" ElementName="featureShape" MonikerTypeName="FeatureShapeMoniker">
        <GeometryShapeMoniker Name="FeatureShape" />
      </XmlClassData>
      <XmlClassData TypeName="ConnectConnector" MonikerAttributeName="" MonikerElementName="connectConnectorMoniker" ElementName="connectConnector" MonikerTypeName="ConnectConnectorMoniker">
        <ConnectorMoniker Name="ConnectConnector" />
      </XmlClassData>
      <XmlClassData TypeName="FeatureModelDSLDiagram" MonikerAttributeName="" MonikerElementName="minimalLanguageDiagramMoniker" ElementName="minimalLanguageDiagram" MonikerTypeName="FeatureModelDSLDiagramMoniker">
        <DiagramMoniker Name="FeatureModelDSLDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="Alternative" MonikerAttributeName="" SerializeId="true" MonikerElementName="alternativeMoniker" ElementName="alternative" MonikerTypeName="AlternativeMoniker">
        <DomainClassMoniker Name="Alternative" />
        <ElementData>
          <XmlPropertyData XmlName="min">
            <DomainPropertyMoniker Name="Alternative/Min" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="max">
            <DomainPropertyMoniker Name="Alternative/Max" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="minMaxIntervalText" Representation="Ignore">
            <DomainPropertyMoniker Name="Alternative/MinMaxIntervalText" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="AlternativeShape" MonikerAttributeName="" MonikerElementName="alternativeShapeMoniker" ElementName="alternativeShape" MonikerTypeName="AlternativeShapeMoniker">
        <ImageShapeMoniker Name="AlternativeShape" />
      </XmlClassData>
      <XmlClassData TypeName="FeatureModelElement" MonikerAttributeName="" MonikerElementName="featureModelElementMoniker" ElementName="featureModelElement" MonikerTypeName="FeatureModelElementMoniker">
        <DomainClassMoniker Name="FeatureModelElement" />
        <ElementData>
          <XmlRelationshipData RoleElementName="subFeatureModelElements">
            <DomainRelationshipMoniker Name="FeatureModelElementReferencesSubFeatureModelElements" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="FeatureModelElementReferencesSubFeatureModelElements" MonikerAttributeName="" MonikerElementName="featureModelElementReferencesSubFeatureModelElementsMoniker" ElementName="featureModelElementReferencesSubFeatureModelElements" MonikerTypeName="FeatureModelElementReferencesSubFeatureModelElementsMoniker">
        <DomainRelationshipMoniker Name="FeatureModelElementReferencesSubFeatureModelElements" />
      </XmlClassData>
      <XmlClassData TypeName="Constrains" MonikerAttributeName="" MonikerElementName="constrainsMoniker" ElementName="constrains" MonikerTypeName="ConstrainsMoniker">
        <DomainRelationshipMoniker Name="Constrains" />
        <ElementData>
          <XmlPropertyData XmlName="constraint">
            <DomainPropertyMoniker Name="Constrains/Constraint" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="ConstraintConnector" MonikerAttributeName="" MonikerElementName="constraintConnectorMoniker" ElementName="constraintConnector" MonikerTypeName="ConstraintConnectorMoniker">
        <ConnectorMoniker Name="ConstraintConnector" />
      </XmlClassData>
      <XmlClassData TypeName="FeatureModel" MonikerAttributeName="name" MonikerElementName="featureModelMoniker" ElementName="featureModel" MonikerTypeName="FeatureModelMoniker">
        <DomainClassMoniker Name="FeatureModel" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="FeatureModel/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="description">
            <DomainPropertyMoniker Name="FeatureModel/Description" />
          </XmlPropertyData>
          <XmlRelationshipData RoleElementName="featureModelElements">
            <DomainRelationshipMoniker Name="FeatureModelHasFeatureModelElements" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="parentFeatureModelFile">
            <DomainPropertyMoniker Name="FeatureModel/ParentFeatureModelFile" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="FeatureModelHasFeatureModelElements" MonikerAttributeName="" MonikerElementName="featureModelHasFeatureModelElementsMoniker" ElementName="featureModelHasFeatureModelElements" MonikerTypeName="FeatureModelHasFeatureModelElementsMoniker">
        <DomainRelationshipMoniker Name="FeatureModelHasFeatureModelElements" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="FeatureModelDSLExplorer" />
  <ConnectionBuilders>
    <ConnectionBuilder Name="FeatureModelElementReferencesSubFeatureModelElementsBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="FeatureModelElementReferencesSubFeatureModelElements" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="FeatureModelElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="FeatureModelElement" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="ConstrainsBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="Constrains" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Feature" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="Feature" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="76d85c20-5ea3-4b26-b5f2-e3fc10915340" Description="Description for UFPE.FeatureModelDSL.FeatureModelDSLDiagram" Name="FeatureModelDSLDiagram" DisplayName="Minimal Language Diagram" Namespace="UFPE.FeatureModelDSL">
    <Class>
      <DomainClassMoniker Name="FeatureModel" />
    </Class>
    <ShapeMaps>
      <ShapeMap>
        <DomainClassMoniker Name="Feature" />
        <ParentElementPath>
          <DomainPath>FeatureModelHasFeatureModelElements.FeatureModel/!FeatureModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <IconDecoratorMoniker Name="FeatureShape/MandatoryDecorator" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="Feature/Occurence" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="Mandatory" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="FeatureShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Feature/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="FeatureShape/OptionalDecorator" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="Feature/Occurence" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="Optional" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <DecoratorMap>
          <IconDecoratorMoniker Name="FeatureShape/ReferenceDecorator" />
          <VisibilityPropertyPath>
            <DomainPropertyMoniker Name="Feature/IsReference" />
            <PropertyFilters>
              <PropertyFilter FilteringValue="True" />
            </PropertyFilters>
          </VisibilityPropertyPath>
        </DecoratorMap>
        <GeometryShapeMoniker Name="FeatureShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="Alternative" />
        <ParentElementPath>
          <DomainPath>FeatureModelHasFeatureModelElements.FeatureModel/!FeatureModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="AlternativeShape/MinMaxDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Alternative/MinMaxIntervalText" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <ImageShapeMoniker Name="AlternativeShape" />
      </ShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="ConnectConnector" />
        <DomainRelationshipMoniker Name="FeatureModelElementReferencesSubFeatureModelElements" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="ConstraintConnector" />
        <DomainRelationshipMoniker Name="Constrains" />
        <DecoratorMap>
          <TextDecoratorMoniker Name="ConstraintConnector/ConstraintDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="Constrains/Constraint" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer FileExtension="fm" EditorGuid="462dce01-ed90-4d48-876f-a2101b91e3bf">
    <RootClass>
      <DomainClassMoniker Name="FeatureModel" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="FeatureModelDSLSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="FeatureModelDSL">
      <ElementTool Name="Feature" ToolboxIcon="Resources\FeatureIcon.bmp" Caption="Feature" Tooltip="Adds a new feature" HelpKeyword="">
        <DomainClassMoniker Name="Feature" />
      </ElementTool>
      <ConnectionTool Name="Connect" ToolboxIcon="Resources\ConnectIcon.bmp" Caption="Connect" Tooltip="Connects a feature with a subfeature" HelpKeyword="">
        <ConnectionBuilderMoniker Name="FeatureModelDSL/FeatureModelElementReferencesSubFeatureModelElementsBuilder" />
      </ConnectionTool>
      <ElementTool Name="Alternative" ToolboxIcon="Resources\AlternativeIcon.bmp" Caption="Alternative" Tooltip="Adds an alternative group" HelpKeyword="Alternative">
        <DomainClassMoniker Name="Alternative" />
      </ElementTool>
      <ConnectionTool Name="Constraint" ToolboxIcon="Resources\ConstraintIcon.bmp" Caption="Constraint" Tooltip="Adds a constraint between two features" HelpKeyword="Constraint">
        <ConnectionBuilderMoniker Name="FeatureModelDSL/ConstrainsBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <Validation UsesMenu="true" UsesOpen="false" UsesSave="true" UsesLoad="false" />
    <DiagramMoniker Name="FeatureModelDSLDiagram" />
  </Designer>
  <Explorer ExplorerGuid="2491ec2e-9b53-41dc-bc99-21a4637ce006" Title="FeatureModelDSL Explorer">
    <ExplorerBehaviorMoniker Name="FeatureModelDSL/FeatureModelDSLExplorer" />
  </Explorer>
</Dsl>